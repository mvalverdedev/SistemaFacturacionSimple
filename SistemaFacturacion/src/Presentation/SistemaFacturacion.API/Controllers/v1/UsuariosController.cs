using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Usuarios.Commands.ActualizarUsuario;
using SistemaFacturacion.Application.Features.Usuarios.Commands.CrearUsuario;
using SistemaFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados;
using System.Threading.Tasks;

namespace SistemaFacturacion.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UsuariosController : ControladorBaseApi
    {
        [HttpGet]
        public async Task<IActionResult> Listado([FromQuery] ObtenerUsuariosPaginadosQuery filtro)
        {
            return Ok(await Mediator.Send(filtro));
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearUsuarioCommand comando)
        {
            return Ok(await Mediator.Send(comando));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarUsuarioCommand comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(comando));
        }
    }
}
