import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: false
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  cargando = false;
  returnUrl: string = '/';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService
  ) {

    if (this.authService.valorUsuarioActual) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      usuario: ['', Validators.required],
      contrasenia: ['', Validators.required]
    });


    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }


  get f() { return this.loginForm.controls; }

  ingresar() {
    if (this.loginForm.invalid) {
      return;
    }

    this.cargando = true;
    this.authService.iniciarSesion(this.f['usuario'].value, this.f['contrasenia'].value)
      .subscribe({
        next: (response) => {
          if (response) {
            this.router.navigate([this.returnUrl]);
          } else {
            this.toastr.error('Respuesta de login inválida', 'Error');
            this.cargando = false;
          }
        },
        error: error => {
          this.toastr.error('Login fallido', 'Error');
          this.cargando = false;
        }
      });
  }
}
