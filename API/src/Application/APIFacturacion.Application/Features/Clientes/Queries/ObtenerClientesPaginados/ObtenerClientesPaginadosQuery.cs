using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace APIFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados
{
    public class ObtenerClientesPaginadosQuery : ClienteParametros, IRequest<RespuestaPaginada<IReadOnlyList<ClienteDto>>>
    {
    }
}

