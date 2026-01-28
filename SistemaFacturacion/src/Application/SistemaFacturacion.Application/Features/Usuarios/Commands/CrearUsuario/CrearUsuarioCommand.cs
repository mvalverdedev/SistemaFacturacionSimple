using MediatR;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Wrappers;

namespace SistemaFacturacion.Application.Features.Usuarios.Commands.CrearUsuario
{
    public class CrearUsuarioCommand : IRequest<Respuesta<int>>
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
    }
}
