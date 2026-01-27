using System;

namespace SistemaFacturacion.Application.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
