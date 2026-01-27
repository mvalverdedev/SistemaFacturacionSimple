using SistemaFacturacion.Domain.Common;

namespace SistemaFacturacion.Domain.Entities
{
    public class Usuario : EntidadBase
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
