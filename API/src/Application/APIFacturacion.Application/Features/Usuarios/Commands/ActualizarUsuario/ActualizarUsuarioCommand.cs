using MediatR;
using APIFacturacion.Application.Wrappers;

namespace APIFacturacion.Application.Features.Usuarios.Commands.ActualizarUsuario
{
    public class ActualizarUsuarioCommand : IRequest<Respuesta<int>>
    {
        public int Id { get; set; }
        public string? Clave { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
    }
}

