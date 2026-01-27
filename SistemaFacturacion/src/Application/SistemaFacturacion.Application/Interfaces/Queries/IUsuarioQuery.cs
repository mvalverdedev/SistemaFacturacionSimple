using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados;

namespace SistemaFacturacion.Application.Interfaces.Queries
{
    public interface IUsuarioQuery
    {
        Task<int> ObtenerTotalRegistrosAsync(ObtenerUsuariosPaginadosQuery request);
        Task<IReadOnlyList<UsuarioDto>> ObtenerUsuariosPaginadosAsync(ObtenerUsuariosPaginadosQuery request);
    }
}
