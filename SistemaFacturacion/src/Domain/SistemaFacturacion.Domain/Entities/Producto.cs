using SistemaFacturacion.Domain.Common;

namespace SistemaFacturacion.Domain.Entities
{
    public class Producto : EntidadBase
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }

    }
}
