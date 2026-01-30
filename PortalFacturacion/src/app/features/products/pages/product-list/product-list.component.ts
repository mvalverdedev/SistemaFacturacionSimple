import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ProductService, Producto } from '../../services/product.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  standalone: false
})
export class ProductListComponent implements OnInit {
  displayedColumns: string[] = ['codigo', 'nombre', 'precioUnitario', 'stock', 'acciones'];
  dataSource = new MatTableDataSource<Producto>();
  totalRecords = 0;
  pageSize = 10;
  pageIndex = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  searchForm!: FormGroup;

  constructor(
    private productService: ProductService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.searchForm = this.fb.group({
      codigo: [''],
      nombre: ['']
    });
  }

  ngOnInit(): void {
    this.cargarProductos();
  }

  cargarProductos() {
    const filters = this.searchForm.value;
    this.productService.obtenerProductos(
      this.pageIndex + 1,
      this.pageSize,
      filters.codigo || undefined,
      filters.nombre || undefined
    )
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.datos;
          this.totalRecords = response.totalRecords;
        },
        error: (err) => {
          this.toastr.error('Error al cargar productos', 'Error');
        }
      });
  }

  onSearch() {
    this.pageIndex = 0;
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.cargarProductos();
  }

  editarProducto(producto: Producto) {
    this.productService.establecerProductoSeleccionado(producto);
    this.router.navigate(['/productos/editar', producto.id]);
  }

  cambiarPagina(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.cargarProductos();
  }
}
