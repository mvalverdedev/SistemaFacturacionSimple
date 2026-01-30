using System.Collections.Generic;
using System.Threading.Tasks;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados;

namespace APIFacturacion.Application.Interfaces.Queries
{
    public interface IUsuarioQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerUsuariosPaginadosQuery request);
        Task<IReadOnlyList<UsuarioDto>> ObtenerUsuariosPaginadosAsync(ObtenerUsuariosPaginadosQuery request);
    }
}

