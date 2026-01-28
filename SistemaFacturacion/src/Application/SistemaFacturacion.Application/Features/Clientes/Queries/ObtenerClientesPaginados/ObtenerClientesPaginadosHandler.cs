using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Interfaces.Queries;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados
{
    public class ObtenerClientesPaginadosHandler : IRequestHandler<ObtenerClientesPaginadosQuery, RespuestaPaginada<IReadOnlyList<ClienteDto>>>
    {
        private readonly IClienteQuery _clienteQuery;

        public ObtenerClientesPaginadosHandler(IClienteQuery clienteQuery)
        {
            _clienteQuery = clienteQuery;
        }

        public async Task<RespuestaPaginada<IReadOnlyList<ClienteDto>>> Handle(ObtenerClientesPaginadosQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteQuery.ObtenerClientesPaginadosAsync(request);
            var totalRegistros = await _clienteQuery.ObtenerTotalRegistrosAsync(request);

            return new RespuestaPaginada<IReadOnlyList<ClienteDto>>(clientes, request.PageNumber, request.PageSize, totalRegistros);
        }
    }
}
