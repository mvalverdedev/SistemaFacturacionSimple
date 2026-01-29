import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
  standalone: false
})
export class UserFormComponent implements OnInit {
  form!: FormGroup;
  esEdicion = false;
  id: number | null = null;
  cargando = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.id = +idParam;
      this.esEdicion = true;
    }
    this.initForm();

    if (this.esEdicion && this.id) {
      this.cargarUsuario(this.id);
    }
  }

  initForm() {
    this.form = this.fb.group({
      nombreUsuario: ['', Validators.required],
      nombreCompleto: ['', Validators.required],
      clave: [this.esEdicion ? '' : '', this.esEdicion ? [] : [Validators.required]],
      rol: ['Vendedor', Validators.required],
      activo: [true]
    });
  }

  cargarUsuario(id: number) {
    this.userService.obtenerUsuarioPorId(id).subscribe({
      next: (response) => {
        if (response.succeeded && response.datos) {
          const usuario = response.datos;
          this.form.patchValue({
            nombreUsuario: usuario.nombreUsuario,
            nombreCompleto: usuario.nombreCompleto,
            rol: usuario.rol,
            activo: usuario.activo,
            clave: ''
          });
        } else {
          this.toastr.error('No se pudo cargar la informacion del usuario');
          this.router.navigate(['/usuarios']);
        }
      },
      error: () => {
        this.toastr.error('Error al cargar usuario');
        this.router.navigate(['/usuarios']);
      }
    });
  }

  guardar() {
    if (this.form.invalid) return;
    this.cargando = true;
    const formData = this.form.value;

    if (this.esEdicion && this.id) {
      const userId = +this.id;
      // Preparar payload para actualizacion (sin nombreUsuario, que el backend no lo espera)
      // IMPORTANTE: Usar PascalCase para coincidir con el backend C#
      const usuario: any = {
        Id: userId,
        NombreCompleto: formData.nombreCompleto,
        Rol: formData.rol,
        Activo: formData.activo
      };
      // Solo enviar clave si fue modificada
      if (formData.clave && formData.clave.trim() !== '') {
        usuario.Clave = formData.clave;
      }
      this.userService.actualizarUsuario(userId, usuario).subscribe({
        next: () => {
          this.toastr.success('Usuario actualizado');
          this.userService.limpiarUsuarioSeleccionado();
          this.router.navigate(['/usuarios']);
        },
        error: () => {
          this.toastr.error('Error al actualizar');
          this.cargando = false;
        }
      });
    } else {
      // Crear nuevo usuario
      const usuario = {
        nombreUsuario: formData.nombreUsuario,
        nombreCompleto: formData.nombreCompleto,
        clave: formData.clave,
        rol: formData.rol,
        activo: formData.activo
      };
      this.userService.crearUsuario(usuario).subscribe({
        next: () => {
          this.toastr.success('Usuario creado');
          this.userService.limpiarUsuarioSeleccionado();
          this.router.navigate(['/usuarios']);
        },
        error: () => {
          this.toastr.error('Error al crear');
          this.cargando = false;
        }
      });
    }
  }
}
