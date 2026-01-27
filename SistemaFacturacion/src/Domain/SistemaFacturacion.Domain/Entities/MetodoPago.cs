using SistemaFacturacion.Domain.Common;

namespace SistemaFacturacion.Domain.Entities
{
    public class MetodoPago : EntidadBase
    {
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
