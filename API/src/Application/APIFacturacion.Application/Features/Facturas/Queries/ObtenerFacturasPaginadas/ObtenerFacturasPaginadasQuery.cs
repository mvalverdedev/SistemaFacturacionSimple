using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace APIFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas
{
    public class ObtenerFacturasPaginadasQuery : FacturaParametros, IRequest<RespuestaPaginada<IReadOnlyList<FacturaDto>>>
    {
    }
}

