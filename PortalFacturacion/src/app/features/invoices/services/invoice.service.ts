import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Respuesta, RespuestaPaginada } from '../../../core/models/api-response.model';

export interface MetodoPago {
  id: string;
  nombre: string;
}

export interface DetalleFactura {
  idProducto: number;
  nombreProducto: string;
  cantidad: number;
  precioUnitario: number;
  subTotal: number;
}

export interface PagoFactura {
  formaPago: string;
  monto: number;
}

export interface Factura {
  id: number;
  numeroFactura: string;
  nombreCliente: string;
  nombreVendedor: string;
  total: number;
  fechaCreacion: string;

  detalles?: DetalleFactura[];
  pagos?: PagoFactura[];
}

export interface CrearFacturaComando {
  clienteId: string;
  usuarioId: string;
  metodoPagoId: string;
  detalles: {
    productoId: string;
    cantidad: number;
    precioUnitario: number;
  }[];
}

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private apiUrl = `${environment.apiUrl}/Facturas`;
  private metodosPagoUrl = `${environment.apiUrl}/MetodosPago`;

  constructor(private http: HttpClient) { }

  obtenerFacturas(pageNumber: number = 1, pageSize: number = 10, numeroFactura?: string, fechaCreacion?: string, total?: number): Observable<RespuestaPaginada<Factura>> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    if (numeroFactura) {
      params = params.set('NumeroFactura', numeroFactura);
    }

    if (fechaCreacion) {
      params = params.set('FechaCreacion', fechaCreacion);
    }

    if (total) {
      params = params.set('Total', total.toString());
    }

    return this.http.get<RespuestaPaginada<Factura>>(this.apiUrl, { params });
  }

  obtenerFacturaPorId(id: string): Observable<Factura> {
    return this.http.get<Factura>(`${this.apiUrl}/${id}`);
  }

  crearFactura(factura: CrearFacturaComando): Observable<Respuesta<string>> {
    return this.http.post<Respuesta<string>>(this.apiUrl, factura);
  }

  obtenerMetodosPago(): Observable<RespuestaPaginada<MetodoPago>> {

    let params = new HttpParams().set('TraerTodo', 'true');
    return this.http.get<RespuestaPaginada<MetodoPago>>(this.metodosPagoUrl, { params });
  }
}
