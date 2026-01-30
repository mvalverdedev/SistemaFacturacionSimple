using APIFacturacion.Domain.Common;

namespace APIFacturacion.Domain.Entities
{
    public class Usuario : EntidadBase
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }


    }
}

