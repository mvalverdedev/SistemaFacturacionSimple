import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InvoiceService, MetodoPago, Factura } from '../../services/invoice.service';
import { ClientService, Cliente } from '../../../clients/services/client.service';
import { ProductService, Producto } from '../../../products/services/product.service';
import { UserService, Usuario } from '../../../users/services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-invoice-form',
  templateUrl: './invoice-form.component.html',
  styleUrls: ['./invoice-form.component.scss'],
  standalone: false
})
export class InvoiceFormComponent implements OnInit {
  form!: FormGroup;
  esVer = false;
  id: string | null = null;
  cargando = false;

  clientes: Cliente[] = [];
  productos: Producto[] = [];
  usuarios: Usuario[] = [];
  metodosPago: MetodoPago[] = [];
  facturarPorId?: Factura;

  constructor(
    private fb: FormBuilder,
    private invoiceService: InvoiceService,
    private clientService: ClientService,
    private productService: ProductService,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.cargarCatalogos();

    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.esVer = true;
      this.form.disable();
      this.cargarFactura(this.id);
    }
  }

  initForm() {
    this.form = this.fb.group({
      numeroFactura: ['', Validators.required],
      clienteId: ['', Validators.required],
      usuarioId: ['', Validators.required],
      detalles: this.fb.array([]),
      pagos: this.fb.array([]),
      total: [{ value: 0, disabled: true }],
      totalPagos: [{ value: 0, disabled: true }]
    });
  }

  get detalles() {
    return this.form.get('detalles') as FormArray;
  }

  get pagos() {
    return this.form.get('pagos') as FormArray;
  }

  cargarCatalogos() {
    // Cargar Clientes
    this.clientService.obtenerClientes(1, 100).subscribe(res => this.clientes = res.datos);
    // Cargar Productos
    this.productService.obtenerProductos(1, 100).subscribe(res => this.productos = res.datos);
    // Cargar Usuarios (Vendedores)
    this.userService.obtenerUsuarios(1, 100).subscribe(res => {
      this.usuarios = res.datos; // We could filter by role here if needed
    });
    // Cargar Metodos Pago
    this.invoiceService.obtenerMetodosPago().subscribe(res => this.metodosPago = res.datos);
  }

  agregarDetalle() {
    const detalle = this.fb.group({
      productoId: ['', Validators.required],
      cantidad: [1, [Validators.required, Validators.min(1)]],
      precioUnitario: [{ value: 0, disabled: false }, Validators.required],
      subtotal: [{ value: 0, disabled: true }],
      productoNombre: [''] // Helper for view
    });
    this.detalles.push(detalle);
  }

  eliminarDetalle(index: number) {
    this.detalles.removeAt(index);
    this.calcularTotal();
  }

  productoSeleccionado(index: number, productoId: number) {
    const producto = this.productos.find(p => p.id === productoId);
    if (producto) {
      const detalle = this.detalles.at(index);
      detalle.patchValue({
        precioUnitario: producto.precioUnitario,
        productoNombre: producto.nombre
      });

      // Validar Stock (Simple validation)
      const cantidad = detalle.get('cantidad')?.value;
      if (cantidad > producto.stock) {
        this.toastr.warning(`Stock insuficiente. Disponible: ${producto.stock}`);
        detalle.patchValue({ cantidad: producto.stock });
      }
      this.calcularTotal();
    }
  }

  calcularSubtotal(index: number) {
    const detalle = this.detalles.at(index);
    const productoId = detalle.get('productoId')?.value;
    const producto = this.productos.find(p => p.id === productoId);

    if (producto) {
      const cantidad = detalle.get('cantidad')?.value;
      if (cantidad > producto.stock) {
        this.toastr.warning(`Stock insuficiente. Disponible: ${producto.stock}`);
        detalle.patchValue({ cantidad: producto.stock });
      }
    }
    this.calcularTotal();
  }

  calcularTotal() {
    let total = 0;
    this.detalles.controls.forEach(control => {
      const cantidad = control.get('cantidad')?.value || 1;
      const precio = control.get('precioUnitario')?.value || 0;
      const subtotal = cantidad * precio;
      control.patchValue({ subtotal }, { emitEvent: false });
      total += subtotal;
    });
    this.form.patchValue({ total });
    this.validarPagos();
  }

  agregarPago() {
    const pago = this.fb.group({
      metodoPagoId: ['', Validators.required],
      monto: [0, [Validators.required, Validators.min(0.01)]]
    });
    this.pagos.push(pago);
  }

  eliminarPago(index: number) {
    this.pagos.removeAt(index);
    this.calcularTotalPagos();
  }

  calcularTotalPagos() {
    let totalPagos = 0;
    this.pagos.controls.forEach(control => {
      const monto = control.get('monto')?.value || 0;
      totalPagos += monto;
    });
    this.form.patchValue({ totalPagos });
    this.validarPagos();
  }

  validarPagos() {
    const total = this.form.get('total')?.value || 0;
    const totalPagos = this.form.get('totalPagos')?.value || 0;

    if (total > 0 && Math.abs(total - totalPagos) > 0.01) {
      this.form.setErrors({ pagosMismatch: true });
    } else {
      if (this.form.hasError('pagosMismatch')) {
        this.form.setErrors(null);
      }
    }
  }

  cargarFactura(id: string) {
    this.invoiceService.obtenerFacturaPorId(id).subscribe({
      next: (fac) => {
        this.facturarPorId = fac;
        this.form.patchValue({
          numeroFactura: fac.numeroFactura,
          total: fac.total,
          totalPagos: fac.total // Asumimos que si está facturada, los pagos cuadran
        });

        // Reconstruct details
        if (fac.detalles && fac.detalles.length > 0) {
          fac.detalles.forEach(d => {
            const det = this.fb.group({
              productoId: [d.idProducto],
              cantidad: [d.cantidad],
              precioUnitario: [d.precioUnitario],
              subtotal: [d.subTotal],
              productoNombre: [d.nombreProducto]
            });
            this.detalles.push(det);
          });
        }

        // Reconstruct pagos
        if (fac.pagos && fac.pagos.length > 0) {
          fac.pagos.forEach(p => {
            const pago = this.fb.group({
              metodoPagoId: [null], // No viene ID en la respuesta de detalle
              monto: [p.monto],
              metodoPagoNombre: [p.formaPago]
            });
            this.pagos.push(pago);
          });
        }
      },
      error: () => {
        this.toastr.error('Error al cargar factura');
      }
    });
  }

  guardar() {
    if (this.form.invalid) {
      this.toastr.error('Complete todos los campos requeridos');
      return;
    }

    // Validar que haya pagos
    if (this.pagos.length === 0) {
      this.toastr.error('Debe agregar al menos una forma de pago');
      return;
    }

    this.cargando = true;

    const formValue = this.form.getRawValue(); // include disabled fields
    // IMPORTANTE: Usar PascalCase para coincidir con el backend C#
    const comando = {
      NumeroFactura: formValue.numeroFactura,
      IdCliente: formValue.clienteId,
      IdUsuario: formValue.usuarioId,
      Total: formValue.total,
      Detalles: formValue.detalles.map((d: any) => ({
        IdProducto: d.productoId,
        Cantidad: d.cantidad,
        PrecioUnitario: d.precioUnitario,
        SubTotal: d.subtotal
      })),
      Pagos: formValue.pagos.map((p: any) => ({
        IdMetodoPago: p.metodoPagoId,
        Monto: p.monto
      }))
    };

    this.invoiceService.crearFactura(comando as any).subscribe({
      next: () => {
        this.toastr.success('Factura creada exitosamente');
        this.router.navigate(['/facturas']);
      },
      error: (err) => {
        this.toastr.error(err.error?.mensaje || 'Error al crear factura');
        this.cargando = false;
      }
    });
  }
}
