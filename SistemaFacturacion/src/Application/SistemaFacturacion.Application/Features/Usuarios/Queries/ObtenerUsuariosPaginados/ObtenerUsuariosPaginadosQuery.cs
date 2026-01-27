using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace SistemaFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados
{
    public class ObtenerUsuariosPaginadosQuery : UsuarioParametros, IRequest<RespuestaPaginada<IReadOnlyList<UsuarioDto>>>
    {
    }
}
