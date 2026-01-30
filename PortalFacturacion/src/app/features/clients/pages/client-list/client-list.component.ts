import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Cliente, ClientService } from '../../services/client.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
  standalone: false
})
export class ClientListComponent implements OnInit {
  displayedColumns: string[] = ['identificacion', 'nombre', 'telefono', 'correo', 'acciones'];
  dataSource = new MatTableDataSource<Cliente>();
  totalRecords = 0;
  pageSize = 10;
  pageIndex = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  searchForm!: FormGroup;

  constructor(
    private clientService: ClientService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.searchForm = this.fb.group({
      nombreRazonSocial: [''],
      identificacion: ['']
    });
  }

  ngOnInit(): void {
    this.cargarClientes();
  }

  cargarClientes() {
    const filters = this.searchForm.value;

    this.clientService.obtenerClientes(
      this.pageIndex + 1,
      this.pageSize,
      filters.nombreRazonSocial || undefined,
      filters.identificacion || undefined
    )
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.datos;
          this.totalRecords = response.totalRecords;
        },
        error: (err) => {
          this.toastr.error('Error al cargar clientes', 'Error');
          console.error(err);
        }
      });
  }

  onSearch() {
    this.pageIndex = 0;
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.cargarClientes();
  }

  editarCliente(cliente: Cliente) {
    this.clientService.establecerClienteSeleccionado(cliente);
    this.router.navigate(['/clientes/editar', cliente.id]);
  }

  cambiarPagina(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.cargarClientes();
  }
}
