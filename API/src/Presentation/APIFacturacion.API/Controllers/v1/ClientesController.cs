using Microsoft.AspNetCore.Mvc;
using APIFacturacion.Application.Features.Clientes.Commands.ActualizarCliente;
using APIFacturacion.Application.Features.Clientes.Commands.CrearCliente;
using APIFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados;
using System.Threading.Tasks;

namespace APIFacturacion.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientesController : ControladorBaseApi
    {
        [HttpGet]
        public async Task<IActionResult> Listado([FromQuery] ObtenerClientesPaginadosQuery filtro)
        {
            return Ok(await Mediator.Send(filtro));
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearClienteComando comando)
        {
            return Ok(await Mediator.Send(comando));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarClienteCommand comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(comando));
        }
    }
}

