using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Facturas.Commands.CrearFactura;
using APIFacturacion.Application.Features.Facturas.Queries.ObtenerFacturaPorId;
using APIFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIFacturacion.API.Controllers.v1
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

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearFacturaCommand comando)
        {
            return Ok(await Mediator.Send(comando));
        }
    }
}

