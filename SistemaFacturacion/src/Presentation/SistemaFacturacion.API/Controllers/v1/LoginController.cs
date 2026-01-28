using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Application.Features.Usuarios.Queries.Login;
using System.Threading.Tasks;

namespace SistemaFacturacion.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class LoginController : ControladorBaseApi
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
