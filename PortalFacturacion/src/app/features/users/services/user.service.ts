import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Respuesta, RespuestaPaginada } from '../../../core/models/api-response.model';

export interface Usuario {
  id: number;
  nombreUsuario: string;
  nombreCompleto: string;
  rol: string;
  activo: boolean;
  fechaCreacion?: string;
  clave?: string; // Solo para crear/actualizar
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/Usuarios`;
  private usuarioSeleccionado: Usuario | null = null;
  private readonly STORAGE_KEY = 'usuarioSeleccionado';

  constructor(private http: HttpClient) {
    this.cargarUsuarioDesdeStorage();
  }

  private cargarUsuarioDesdeStorage() {
    const stored = localStorage.getItem(this.STORAGE_KEY);
    if (stored) {
      try {
        this.usuarioSeleccionado = JSON.parse(stored);
      } catch (e) {
        console.error('Error parsing stored user', e);
        localStorage.removeItem(this.STORAGE_KEY);
      }
    }
  }

  establecerUsuarioSeleccionado(usuario: Usuario) {
    this.usuarioSeleccionado = usuario;
    localStorage.setItem(this.STORAGE_KEY, JSON.stringify(usuario));
  }

  limpiarUsuarioSeleccionado() {
    this.usuarioSeleccionado = null;
    localStorage.removeItem(this.STORAGE_KEY);
  }

  obtenerUsuarios(pageNumber: number = 1, pageSize: number = 10): Observable<RespuestaPaginada<Usuario>> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    return this.http.get<RespuestaPaginada<Usuario>>(this.apiUrl, { params });
  }

  obtenerUsuarioPorId(id: number): Observable<Respuesta<Usuario>> {
    if (this.usuarioSeleccionado && this.usuarioSeleccionado.id === id) {
      return of({
        succeeded: true,
        mensaje: '',
        errores: [],
        datos: this.usuarioSeleccionado,
        pageNumber: 1,
        pageSize: 1,
        totalRecords: 1,
        totalPages: 1
      } as Respuesta<Usuario>);
    }
    return this.http.get<Respuesta<Usuario>>(`${this.apiUrl}/${id}`);
  }

  crearUsuario(usuario: Partial<Usuario>): Observable<Respuesta<string>> {
    return this.http.post<Respuesta<string>>(this.apiUrl, usuario);
  }

  actualizarUsuario(id: number, usuario: Partial<Usuario>): Observable<Respuesta<boolean>> {
    return this.http.put<Respuesta<boolean>>(`${this.apiUrl}/${id}`, usuario);
  }
}
