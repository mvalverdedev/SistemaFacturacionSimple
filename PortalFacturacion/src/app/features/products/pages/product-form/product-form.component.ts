import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss'],
  standalone: false
})
export class ProductFormComponent implements OnInit {
  form!: FormGroup;
  esEdicion = false;
  id: number | null = null;
  cargando = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.initForm();
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.id = +idParam;
      this.esEdicion = true;
      this.cargarProducto(this.id);
    }
  }

  initForm() {
    this.form = this.fb.group({
      codigo: ['', Validators.required],
      nombre: ['', Validators.required],
      precioUnitario: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      activo: [true]
    });
  }

  cargarProducto(id: number) {
    this.productService.obtenerProductoPorId(id).subscribe({
      next: (response) => {
        if (response.succeeded && response.datos) {
          const producto = response.datos;
          this.form.patchValue({
            codigo: producto.codigo,
            nombre: producto.nombre,
            precioUnitario: producto.precioUnitario,
            stock: producto.stock,
            activo: producto.activo
          });
        } else {
          this.toastr.error('No se pudo cargar la informacion del producto');
          this.router.navigate(['/productos']);
        }
      },
      error: () => {
        this.toastr.error('Error al cargar producto');
        this.router.navigate(['/productos']);
      }
    });
  }

  guardar() {
    if (this.form.invalid) return;
    this.cargando = true;
    const formData = this.form.value;

    if (this.esEdicion && this.id) {
      const productId = +this.id;


      const producto = {
        Id: productId,
        Nombre: formData.nombre,
        PrecioUnitario: formData.precioUnitario,
        Stock: formData.stock,
        Activo: formData.activo
      };
      this.productService.actualizarProducto(productId, producto as any).subscribe({
        next: () => {
          this.toastr.success('Producto actualizado');
          this.productService.limpiarProductoSeleccionado();
          this.router.navigate(['/productos']);
        },
        error: () => {
          this.toastr.error('Error al actualizar');
          this.cargando = false;
        }
      });
    } else {

      const producto = {
        codigo: formData.codigo,
        nombre: formData.nombre,
        precioUnitario: formData.precioUnitario,
        stock: formData.stock,
        activo: formData.activo
      };
      this.productService.crearProducto(producto).subscribe({
        next: () => {
          this.toastr.success('Producto creado');
          this.productService.limpiarProductoSeleccionado();
          this.router.navigate(['/productos']);
        },
        error: () => {
          this.toastr.error('Error al crear');
          this.cargando = false;
        }
      });
    }
  }
}
