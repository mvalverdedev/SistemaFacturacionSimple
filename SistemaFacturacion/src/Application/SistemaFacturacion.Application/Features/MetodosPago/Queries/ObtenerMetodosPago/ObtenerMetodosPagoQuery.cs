using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace SistemaFacturacion.Application.Features.MetodosPago.Queries.ObtenerMetodosPago
{
    public class ObtenerMetodosPagoQuery : IRequest<Respuesta<IReadOnlyList<MetodoPagoDto>>>
    {
    }
}
