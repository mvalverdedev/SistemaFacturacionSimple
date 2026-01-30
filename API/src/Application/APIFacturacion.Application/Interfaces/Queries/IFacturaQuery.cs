using System.Collections.Generic;
using System.Threading.Tasks;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas;

namespace APIFacturacion.Application.Interfaces.Queries
{
    public interface IFacturaQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerFacturasPaginadasQuery request);
        Task<IReadOnlyList<FacturaDto>> ObtenerFacturasPaginadasAsync(ObtenerFacturasPaginadasQuery request);
        Task<FacturaDetalleDto> ObtenerFacturaPorIdAsync(int id);
    }
}

