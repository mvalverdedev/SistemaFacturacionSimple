using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas;

namespace SistemaFacturacion.Application.Interfaces.Queries
{
    public interface IFacturaQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerFacturasPaginadasQuery request);
        Task<IReadOnlyList<FacturaDto>> ObtenerFacturasPaginadasAsync(ObtenerFacturasPaginadasQuery request);
        Task<FacturaDetalleDto> ObtenerFacturaPorIdAsync(int id);
    }
}
