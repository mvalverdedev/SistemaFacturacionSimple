using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Interfaces.Queries;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APIFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados
{
    public class ObtenerUsuariosPaginadosHandler : IRequestHandler<ObtenerUsuariosPaginadosQuery, RespuestaPaginada<IReadOnlyList<UsuarioDto>>>
    {
        private readonly IUsuarioQuery _usuarioQuery;

        public ObtenerUsuariosPaginadosHandler(IUsuarioQuery usuarioQuery)
        {
            _usuarioQuery = usuarioQuery;
        }

        public async Task<RespuestaPaginada<IReadOnlyList<UsuarioDto>>> Handle(ObtenerUsuariosPaginadosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioQuery.ObtenerUsuariosPaginadosAsync(request);
            var totalRegistros = await _usuarioQuery.ObtenerTotalRegistrosAsync(request);

            return new RespuestaPaginada<IReadOnlyList<UsuarioDto>>(usuarios, request.PageNumber, request.PageSize, totalRegistros);
        }
    }
}

