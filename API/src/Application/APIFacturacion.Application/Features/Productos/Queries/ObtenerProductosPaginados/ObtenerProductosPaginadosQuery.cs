using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace APIFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados
{
    public class ObtenerProductosPaginadosQuery : ProductoParametros, IRequest<RespuestaPaginada<IReadOnlyList<ProductoDto>>>
    {
    }
}

