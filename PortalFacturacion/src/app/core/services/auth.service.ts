import { HttpClient } from '@angular/common/http';
import { LoginResponse } from '../models/login-response.model';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private usuarioActualSubject: BehaviorSubject<LoginResponse | null>;
  public usuarioActual: Observable<LoginResponse | null>;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    this.usuarioActualSubject = new BehaviorSubject<LoginResponse | null>(JSON.parse(localStorage.getItem('usuarioActual')!));
    this.usuarioActual = this.usuarioActualSubject.asObservable();
  }

  public get valorUsuarioActual(): LoginResponse | null {
    return this.usuarioActualSubject.value;
  }

  iniciarSesion(usuario: string, contrasenia: string) {
    return this.http.post<any>(`${environment.apiUrl}/Login`, { NombreUsuario: usuario, Clave: contrasenia })
      .pipe(map(response => {
        console.log('Login response:', response);


        const datos = response.datos || response;


        const token = datos.token || datos.Token;

        if (token) {
          const normalizedUser: LoginResponse = {
            id: datos.id || datos.Id,
            nombreUsuario: datos.nombreUsuario || datos.NombreUsuario,
            nombreCompleto: datos.nombreCompleto || datos.NombreCompleto,
            rol: datos.rol || datos.Rol,
            token: token
          };
          localStorage.setItem('usuarioActual', JSON.stringify(normalizedUser));
          this.usuarioActualSubject.next(normalizedUser);
          return normalizedUser;
        }
        return null;
      }));
  }

  cerrarSesion() {
    localStorage.removeItem('usuarioActual');
    this.usuarioActualSubject.next(null);
    this.router.navigate(['/login']);
  }

  estaAutenticado(): boolean {
    const usuario = this.valorUsuarioActual;
    return !!(usuario && usuario.token);
  }
}
