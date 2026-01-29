using AutoMapper;
using MediatR;
using APIFacturacion.Application.Wrappers;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APIFacturacion.Application.Features.Facturas.Commands.CrearFactura
{
    public class CrearFacturaHandler : IRequestHandler<CrearFacturaCommand, Respuesta<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CrearFacturaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Respuesta<int>> Handle(CrearFacturaCommand request, CancellationToken cancellationToken)
        {
            var nuevaFactura = new Factura
            {
                NumeroFactura = request.NumeroFactura,
                IdCliente = request.IdCliente,
                IdUsuario = request.IdUsuario,
                Total = request.Total,
                FechaCreacion = DateTime.Now
            };

            foreach (var detalleDto in request.Detalles)
            {
                var detalle = new DetalleFactura
                {
                    IdProducto = detalleDto.IdProducto,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = detalleDto.PrecioUnitario,
                    SubTotal = detalleDto.SubTotal,
                    FechaCreacion = DateTime.Now
                };
                nuevaFactura.Detalles.Add(detalle);
            }

            foreach (var pagoDto in request.Pagos)
            {
                var pago = new PagoFactura
                {
                    IdMetodoPago = pagoDto.IdMetodoPago,
                    Monto = pagoDto.Monto,
                    FechaPago = DateTime.Now,
                    FechaCreacion = DateTime.Now
                };
                nuevaFactura.Pagos.Add(pago);
            }

            var facturaRepositorio = _unitOfWork.Repository<Factura>();
            var data = await facturaRepositorio.AgregarAsync(nuevaFactura);

            await _unitOfWork.SaveAsync(cancellationToken);

            return new Respuesta<int>(data.Id, "Factura creada exitosamente");
        }
    }
}
