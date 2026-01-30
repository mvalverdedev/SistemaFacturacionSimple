import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
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

  searchForm!: FormGroup;

  constructor(
    private invoiceService: InvoiceService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {
    this.searchForm = this.fb.group({
      numeroFactura: [''],
      fechaCreacion: [''],
      total: [null]
    });
  }

  ngOnInit(): void {
    this.cargarFacturas();
  }

  cargarFacturas() {
    const filters = this.searchForm.value;
    this.invoiceService.obtenerFacturas(
      this.pageIndex + 1,
      this.pageSize,
      filters.numeroFactura || undefined,
      filters.fechaCreacion ? this.formatDate(filters.fechaCreacion) : undefined,
      filters.total || undefined
    )
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

  onSearch() {
    this.pageIndex = 0;
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.cargarFacturas();
  }

  cambiarPagina(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.cargarFacturas();
  }

  private formatDate(date: Date | string): string {
    if (!date) return '';
    const d = new Date(date);
    let month = '' + (d.getMonth() + 1);
    let day = '' + d.getDate();
    const year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
  }
}
