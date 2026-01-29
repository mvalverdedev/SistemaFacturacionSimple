import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { InvoiceService, Factura } from '../../services/invoice.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.scss'],
  standalone: false
})
export class InvoiceListComponent implements OnInit {
  displayedColumns: string[] = ['numeroFactura', 'fechaCreacion', 'nombreCliente', 'nombreVendedor', 'total', 'acciones'];
  dataSource = new MatTableDataSource<Factura>();
  totalRecords = 0;
  pageSize = 10;
  pageIndex = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private invoiceService: InvoiceService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.cargarFacturas();
  }

  cargarFacturas() {
    this.invoiceService.obtenerFacturas(this.pageIndex + 1, this.pageSize)
      .subscribe({
        next: (response) => {
          if (response.succeeded) {
            this.dataSource.data = response.datos;
            this.totalRecords = response.totalRecords;
          } else {
            this.toastr.error(response.mensaje || 'Error al cargar facturas');
          }
        },
        error: (err) => {
          this.toastr.error('Error al cargar facturas', 'Error');
        }
      });
  }

  cambiarPagina(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.cargarFacturas();
  }
}
