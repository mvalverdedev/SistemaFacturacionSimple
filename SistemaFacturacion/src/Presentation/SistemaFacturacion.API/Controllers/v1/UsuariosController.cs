using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.DTOs;
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
    }
}
