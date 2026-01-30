using System.Collections.Generic;
using System.Threading.Tasks;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados;

namespace APIFacturacion.Application.Interfaces.Queries
{
    public interface IClienteQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerClientesPaginadosQuery request);
        Task<IReadOnlyList<ClienteDto>> ObtenerClientesPaginadosAsync(ObtenerClientesPaginadosQuery request);
    }
}

