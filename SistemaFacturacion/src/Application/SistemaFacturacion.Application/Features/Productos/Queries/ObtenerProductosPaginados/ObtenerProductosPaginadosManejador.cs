using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Interfaces.Queries;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados
{
    public class ObtenerProductosPaginadosManejador : IRequestHandler<ObtenerProductosPaginadosQuery, RespuestaPaginada<IReadOnlyList<ProductoDto>>>
    {
        private readonly IProductoQuery _productoQuery;

        public ObtenerProductosPaginadosManejador(IProductoQuery productoQuery)
        {
            _productoQuery = productoQuery;
        }

        public async Task<RespuestaPaginada<IReadOnlyList<ProductoDto>>> Handle(ObtenerProductosPaginadosQuery request, CancellationToken cancellationToken)
        {
            var productos = await _productoQuery.ObtenerProductosPaginadosAsync(request);
            var totalRegistros = await _productoQuery.ObtenerTotalRegistrosAsync(request);

            return new RespuestaPaginada<IReadOnlyList<ProductoDto>>(productos, request.PageNumber, request.PageSize, totalRegistros);
        }
    }
}
