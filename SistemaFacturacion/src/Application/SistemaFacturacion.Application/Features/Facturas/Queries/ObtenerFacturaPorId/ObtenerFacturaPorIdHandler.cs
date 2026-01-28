using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Interfaces.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturaPorId
{
    /// <summary>
    /// Handler para procesar la consulta de factura por ID
    /// </summary>
    public class ObtenerFacturaPorIdHandler : IRequestHandler<ObtenerFacturaPorIdQuery, FacturaDetalleDto>
    {
        private readonly IFacturaQuery _facturaQuery;

        public ObtenerFacturaPorIdHandler(IFacturaQuery facturaQuery)
        {
            _facturaQuery = facturaQuery;
        }

        public async Task<FacturaDetalleDto> Handle(ObtenerFacturaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _facturaQuery.ObtenerFacturaPorIdAsync(request.Id);
        }
    }
}
