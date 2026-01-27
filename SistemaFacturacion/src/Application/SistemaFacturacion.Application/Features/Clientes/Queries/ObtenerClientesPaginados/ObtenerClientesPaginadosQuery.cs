using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace SistemaFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados
{
    public class ObtenerClientesPaginadosQuery : ClienteParametros, IRequest<RespuestaPaginada<IReadOnlyList<ClienteDto>>>
    {
    }
}
