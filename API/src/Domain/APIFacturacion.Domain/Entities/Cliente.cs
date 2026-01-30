using APIFacturacion.Domain.Common;

namespace APIFacturacion.Domain.Entities
{
    public class Cliente : EntidadBase
    {
        public string NombreRazonSocial { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

    }
}

