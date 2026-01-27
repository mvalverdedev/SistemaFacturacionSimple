using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados;

namespace SistemaFacturacion.Application.Interfaces.Queries
{
    public interface IClienteQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerClientesPaginadosQuery request);
        Task<IReadOnlyList<ClienteDto>> ObtenerClientesPaginadosAsync(ObtenerClientesPaginadosQuery request);
    }
}
