using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Interfaces.Queries;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas
{
    public class ObtenerFacturasPaginadasHandler : IRequestHandler<ObtenerFacturasPaginadasQuery, RespuestaPaginada<IReadOnlyList<FacturaDto>>>
    {
        private readonly IFacturaQuery _facturaQuery;

        public ObtenerFacturasPaginadasHandler(IFacturaQuery facturaQuery)
        {
            _facturaQuery = facturaQuery;
        }

        public async Task<RespuestaPaginada<IReadOnlyList<FacturaDto>>> Handle(ObtenerFacturasPaginadasQuery request, CancellationToken cancellationToken)
        {
            var facturas = await _facturaQuery.ObtenerFacturasPaginadasAsync(request);
            var totalRegistros = await _facturaQuery.ObtenerTotalRegistrosAsync(request);

            return new RespuestaPaginada<IReadOnlyList<FacturaDto>>(facturas, request.PageNumber, request.PageSize, totalRegistros);
        }
    }
}
