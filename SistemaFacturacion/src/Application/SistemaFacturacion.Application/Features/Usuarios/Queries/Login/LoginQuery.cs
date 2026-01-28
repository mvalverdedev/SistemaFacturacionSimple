using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Wrappers;

namespace SistemaFacturacion.Application.Features.Usuarios.Queries.Login
{
    public class LoginQuery : IRequest<Respuesta<LoginResponse>>
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
    }
}
