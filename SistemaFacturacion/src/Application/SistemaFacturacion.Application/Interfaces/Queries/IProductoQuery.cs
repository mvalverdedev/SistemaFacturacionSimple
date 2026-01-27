using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados;

namespace SistemaFacturacion.Application.Interfaces.Queries
{
    public interface IProductoQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerProductosPaginadosQuery request);
        Task<IReadOnlyList<ProductoDto>> ObtenerProductosPaginadosAsync(ObtenerProductosPaginadosQuery request);
    }
}
