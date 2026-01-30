using APIFacturacion.Domain.Common;

namespace APIFacturacion.Domain.Entities
{
    public class PagoFactura : EntidadBase
    {
        public int IdFactura { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        // Propiedades de Navegacion
        public virtual Factura Factura { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
    }
}

