using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.DTOs;
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
    }
}
