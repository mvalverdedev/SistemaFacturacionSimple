using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.Features.MetodosPago.Queries.ObtenerMetodosPago;
using System.Threading.Tasks;

namespace SistemaFacturacion.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MetodosPagoController : ControladorBaseApi
    {
        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            return Ok(await Mediator.Send(new ObtenerMetodosPagoQuery()));
        }
    }
}
