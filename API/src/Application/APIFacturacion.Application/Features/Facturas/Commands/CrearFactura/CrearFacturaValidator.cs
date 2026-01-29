using FluentValidation;

namespace APIFacturacion.Application.Features.Facturas.Commands.CrearFactura
{
    public class CrearFacturaValidator : AbstractValidator<CrearFacturaCommand>
    {
        public CrearFacturaValidator()
        {
            RuleFor(x => x.NumeroFactura)
                .NotEmpty().WithMessage("El numero de factura es requerido")
                .MaximumLength(50).WithMessage("El numero de factura no puede exceder 50 caracteres");

            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("Debe seleccionar un cliente valido");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("Debe seleccionar un usuario valido");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a cero");

            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("La factura debe tener al menos un detalle");

            RuleForEach(x => x.Detalles).ChildRules(detalle =>
            {
                detalle.RuleFor(d => d.IdProducto)
                    .GreaterThan(0).WithMessage("Debe seleccionar un producto valido");

                detalle.RuleFor(d => d.Cantidad)
                    .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero");

                detalle.RuleFor(d => d.PrecioUnitario)
                    .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero");

                detalle.RuleFor(d => d.SubTotal)
                    .GreaterThan(0).WithMessage("El subtotal debe ser mayor a cero");
            });

            RuleFor(x => x.Pagos)
                .NotEmpty().WithMessage("La factura debe tener al menos un pago");

            RuleForEach(x => x.Pagos).ChildRules(pago =>
            {
                pago.RuleFor(p => p.IdMetodoPago)
                    .GreaterThan(0).WithMessage("Debe seleccionar un metodo de pago valido");

                pago.RuleFor(p => p.Monto)
                    .GreaterThan(0).WithMessage("El monto del pago debe ser mayor a cero");
            });
        }
    }
}
