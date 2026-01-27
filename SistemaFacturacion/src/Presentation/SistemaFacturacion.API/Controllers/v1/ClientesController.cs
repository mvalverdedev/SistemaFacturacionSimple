using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados;
using System.Threading.Tasks;

namespace SistemaFacturacion.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientesController : ControladorBaseApi
    {
        [HttpGet]
        public async Task<IActionResult> Listado([FromQuery] ObtenerClientesPaginadosQuery filtro)
        {
            return Ok(await Mediator.Send(filtro));
        }
    }
}
