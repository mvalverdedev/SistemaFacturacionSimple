using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace SistemaFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados
{
    public class ObtenerProductosPaginadosQuery : ProductoParametros, IRequest<RespuestaPaginada<IReadOnlyList<ProductoDto>>>
    {
    }
}
