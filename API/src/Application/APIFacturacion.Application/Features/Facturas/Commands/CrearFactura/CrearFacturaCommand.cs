using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace APIFacturacion.Application.Features.Facturas.Commands.CrearFactura
{
    public class CrearFacturaCommand : IRequest<Respuesta<int>>
    {
        public string NumeroFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public List<DetalleFacturaDto> Detalles { get; set; }
        public List<PagoFacturaDto> Pagos { get; set; }

        public CrearFacturaCommand()
        {
            Detalles = new List<DetalleFacturaDto>();
            Pagos = new List<PagoFacturaDto>();
        }
    }
}
