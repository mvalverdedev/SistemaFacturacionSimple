using MediatR;
using SistemaFacturacion.Application.DTOs;

namespace SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturaPorId
{
    /// <summary>
    /// Query para obtener el detalle completo de una factura por su ID
    /// </summary>
    public class ObtenerFacturaPorIdQuery : IRequest<FacturaDetalleDto>
    {
        public int Id { get; set; }
    }
}
