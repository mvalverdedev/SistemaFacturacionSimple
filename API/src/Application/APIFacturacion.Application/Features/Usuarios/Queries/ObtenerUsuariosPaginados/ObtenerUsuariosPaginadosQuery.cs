using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace APIFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados
{
    public class ObtenerUsuariosPaginadosQuery : UsuarioParametros, IRequest<RespuestaPaginada<IReadOnlyList<UsuarioDto>>>
    {
    }
}

