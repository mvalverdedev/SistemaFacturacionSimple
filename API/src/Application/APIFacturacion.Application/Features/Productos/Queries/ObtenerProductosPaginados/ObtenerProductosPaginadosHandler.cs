using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Interfaces.Queries;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APIFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados
{
    public class ObtenerProductosPaginadosHandler : IRequestHandler<ObtenerProductosPaginadosQuery, RespuestaPaginada<IReadOnlyList<ProductoDto>>>
    {
        private readonly IProductoQuery _productoQuery;

        public ObtenerProductosPaginadosHandler(IProductoQuery productoQuery)
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

