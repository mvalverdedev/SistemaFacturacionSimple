using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace APIFacturacion.Application.Features.MetodosPago.Queries.ObtenerMetodosPago
{
    public class ObtenerMetodosPagoQuery : IRequest<Respuesta<IReadOnlyList<MetodoPagoDto>>>
    {
    }
}

