import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from '../../services/client.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.scss'],
  standalone: false
})
export class ClientFormComponent implements OnInit {
  form!: FormGroup;
  esEdicion = false;
  id: string | null = null;
  cargando = false;

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.esEdicion = true;
      this.cargarCliente(+this.id);
    }
  }

  initForm() {
    this.form = this.fb.group({
      identificacion: ['', Validators.required],
      nombreRazonSocial: ['', Validators.required],
      telefono: [''],
      correo: ['', [Validators.email]],
      direccion: [''],
      activo: [true]
    });
  }

  cargarCliente(id: number) {
    this.cargando = true;
    this.clientService.obtenerClientePorId(id).subscribe({
      next: (response) => {
        if (response.succeeded && response.datos) {

          const data: any = response.datos;
          const cliente = Array.isArray(data) ? data[0] : data;

          this.form.patchValue({
            identificacion: cliente.identificacion,
            nombreRazonSocial: cliente.nombreRazonSocial,
            telefono: cliente.telefono,
            correo: cliente.correo,
            direccion: cliente.direccion,
            activo: cliente.activo
          });
        } else {
          this.toastr.error('No se pudo cargar la información del cliente');
          this.router.navigate(['/clientes']);
        }
        this.cargando = false;
      },
      error: (err) => {
        this.toastr.error('Error al cargar cliente');
        this.cargando = false;
        this.router.navigate(['/clientes']);
      }
    });
  }

  guardar() {
    if (this.form.invalid) return;
    this.cargando = true;
    const formData = this.form.value;

    if (this.esEdicion && this.id) {
      const clientId = +this.id;

      const cliente = {
        Id: clientId,
        NombreRazonSocial: formData.nombreRazonSocial,
        Telefono: formData.telefono,
        Correo: formData.correo,
        Direccion: formData.direccion,
        Activo: formData.activo
      };
      this.clientService.actualizarCliente(clientId, cliente as any).subscribe({
        next: () => {
          this.toastr.success('Cliente actualizado');
          this.clientService.limpiarClienteSeleccionado();
          this.router.navigate(['/clientes']);
        },
        error: () => {
          this.toastr.error('Error al actualizar');
          this.cargando = false;
        }
      });
    } else {

      const cliente = {
        identificacion: formData.identificacion,
        nombreRazonSocial: formData.nombreRazonSocial,
        telefono: formData.telefono,
        correo: formData.correo,
        direccion: formData.direccion,
        activo: formData.activo
      };
      this.clientService.crearCliente(cliente).subscribe({
        next: () => {
          this.toastr.success('Cliente creado');
          this.clientService.limpiarClienteSeleccionado();
          this.router.navigate(['/clientes']);
        },
        error: () => {
          this.toastr.error('Error al crear');
          this.cargando = false;
        }
      });
    }
  }
}
