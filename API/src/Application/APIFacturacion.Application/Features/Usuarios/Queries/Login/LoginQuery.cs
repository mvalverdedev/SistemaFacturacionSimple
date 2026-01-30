using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;

namespace APIFacturacion.Application.Features.Usuarios.Queries.Login
{
    public class LoginQuery : IRequest<Respuesta<LoginResponse>>
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
    }
}

