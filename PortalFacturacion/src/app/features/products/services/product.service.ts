import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Respuesta, RespuestaPaginada } from '../../../core/models/api-response.model';

export interface Producto {
  id: number;
  codigo: string;
  nombre: string;
  precioUnitario: number;
  stock: number;
  activo: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = `${environment.apiUrl}/Productos`;
  private productoSeleccionado: Producto | null = null;
  private readonly STORAGE_KEY = 'productoSeleccionado';

  constructor(private http: HttpClient) {
    this.cargarProductoDesdeStorage();
  }

  private cargarProductoDesdeStorage() {
    const stored = localStorage.getItem(this.STORAGE_KEY);
    if (stored) {
      try {
        this.productoSeleccionado = JSON.parse(stored);
      } catch (e) {
        console.error('Error parsing stored product', e);
        localStorage.removeItem(this.STORAGE_KEY);
      }
    }
  }

  establecerProductoSeleccionado(producto: Producto) {
    this.productoSeleccionado = producto;
    localStorage.setItem(this.STORAGE_KEY, JSON.stringify(producto));
  }

  limpiarProductoSeleccionado() {
    this.productoSeleccionado = null;
    localStorage.removeItem(this.STORAGE_KEY);
  }

  obtenerProductos(pageNumber: number = 1, pageSize: number = 10): Observable<RespuestaPaginada<Producto>> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    return this.http.get<RespuestaPaginada<Producto>>(this.apiUrl, { params });
  }

  obtenerProductoPorId(id: number): Observable<Respuesta<Producto>> {
    if (this.productoSeleccionado && this.productoSeleccionado.id === id) {
      return of({
        succeeded: true,
        mensaje: '',
        errores: [],
        datos: this.productoSeleccionado,
        pageNumber: 1,
        pageSize: 1,
        totalRecords: 1,
        totalPages: 1
      } as Respuesta<Producto>);
    }
    return this.http.get<Respuesta<Producto>>(`${this.apiUrl}/${id}`);
  }

  crearProducto(producto: Partial<Producto>): Observable<Respuesta<string>> {
    return this.http.post<Respuesta<string>>(this.apiUrl, producto);
  }

  actualizarProducto(id: number, producto: Partial<Producto>): Observable<Respuesta<boolean>> {
    return this.http.put<Respuesta<boolean>>(`${this.apiUrl}/${id}`, producto);
  }
}
