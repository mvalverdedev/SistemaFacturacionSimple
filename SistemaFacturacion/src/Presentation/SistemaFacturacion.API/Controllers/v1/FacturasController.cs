using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas;
using SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturaPorId;
using System.Threading.Tasks;

namespace SistemaFacturacion.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FacturasController : ControladorBaseApi
    {
        [HttpGet]
        public async Task<IActionResult> Listado([FromQuery] ObtenerFacturasPaginadasQuery filtro)
        {
            return Ok(await Mediator.Send(filtro));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await Mediator.Send(new ObtenerFacturaPorIdQuery { Id = id });

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }
    }
}
