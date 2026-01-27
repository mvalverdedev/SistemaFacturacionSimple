using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Wrappers;
using System.Collections.Generic;

namespace SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas
{
    public class ObtenerFacturasPaginadasQuery : FacturaParametros, IRequest<RespuestaPaginada<IReadOnlyList<FacturaDto>>>
    {
    }
}
