using System.Collections.Generic;
using System.Threading.Tasks;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados;

namespace APIFacturacion.Application.Interfaces.Queries
{
    public interface IProductoQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerProductosPaginadosQuery request);
        Task<IReadOnlyList<ProductoDto>> ObtenerProductosPaginadosAsync(ObtenerProductosPaginadosQuery request);
    }
}

