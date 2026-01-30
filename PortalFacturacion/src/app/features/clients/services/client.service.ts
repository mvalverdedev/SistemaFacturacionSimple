import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { Respuesta, RespuestaPaginada } from '../../../core/models/api-response.model';

export interface Cliente {
  id: number;
  identificacion: string;
  nombreRazonSocial: string;
  telefono: string;
  correo: string;
  direccion: string;
  activo: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private apiUrl = `${environment.apiUrl}/Clientes`;
  private clienteSeleccionado: Cliente | null = null;
  private readonly STORAGE_KEY = 'clienteSeleccionado';

  constructor(private http: HttpClient) {
    this.cargarClienteDesdeStorage();
  }

  private cargarClienteDesdeStorage() {
    const stored = localStorage.getItem(this.STORAGE_KEY);
    if (stored) {
      try {
        this.clienteSeleccionado = JSON.parse(stored);
      } catch (e) {
        console.error('Error parsing stored client', e);
        localStorage.removeItem(this.STORAGE_KEY);
      }
    }
  }

  establecerClienteSeleccionado(cliente: Cliente) {
    this.clienteSeleccionado = cliente;
    localStorage.setItem(this.STORAGE_KEY, JSON.stringify(cliente));
  }

  limpiarClienteSeleccionado() {
    this.clienteSeleccionado = null;
    localStorage.removeItem(this.STORAGE_KEY);
  }

  obtenerClientes(pageNumber: number = 1, pageSize: number = 10, nombreRazonSocial?: string, identificacion?: string): Observable<RespuestaPaginada<Cliente>> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    if (nombreRazonSocial) {
      params = params.set('NombreRazonSocial', nombreRazonSocial);
    }

    if (identificacion) {
      params = params.set('Identificacion', identificacion);
    }

    return this.http.get<RespuestaPaginada<Cliente>>(this.apiUrl, { params });
  }

  crearCliente(cliente: Partial<Cliente>): Observable<Respuesta<string>> {
    return this.http.post<Respuesta<string>>(this.apiUrl, cliente);
  }

  obtenerClientePorId(id: number): Observable<Respuesta<Cliente>> {
    if (this.clienteSeleccionado && this.clienteSeleccionado.id === id) {
      return of({
        succeeded: true,
        mensaje: '',
        errores: [],
        datos: this.clienteSeleccionado,
        pageNumber: 1,
        pageSize: 1,
        totalRecords: 1,
        totalPages: 1
      } as Respuesta<Cliente>);
    }
    return this.http.get<Respuesta<Cliente>>(`${this.apiUrl}/${id}`);
  }

  actualizarCliente(id: number, cliente: Partial<Cliente>): Observable<Respuesta<boolean>> {
    return this.http.put<Respuesta<boolean>>(`${this.apiUrl}/${id}`, cliente);
  }
}
