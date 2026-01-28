using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Productos.Commands.ActualizarProducto;
using SistemaFacturacion.Application.Features.Productos.Commands.CrearProducto;
using SistemaFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados;
using System.Threading.Tasks;

namespace SistemaFacturacion.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductosController : ControladorBaseApi
    {
        [HttpGet]
        public async Task<IActionResult> Listado([FromQuery] ObtenerProductosPaginadosQuery filtro)
        {
            return Ok(await Mediator.Send(filtro));
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProductoCommand comando)
        {
            return Ok(await Mediator.Send(comando));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoCommand comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(comando));
        }
    }
}
