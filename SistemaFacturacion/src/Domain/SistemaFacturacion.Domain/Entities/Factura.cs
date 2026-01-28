using SistemaFacturacion.Domain.Common;
using System.Collections.Generic;

namespace SistemaFacturacion.Domain.Entities
{
    public class Factura : EntidadBase
    {
        public string NumeroFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }


        // Propiedades de Navegacion
        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<DetalleFactura> Detalles { get; set; }
        public virtual ICollection<PagoFactura> Pagos { get; set; }

        public Factura()
        {
            Detalles = new HashSet<DetalleFactura>();
            Pagos = new HashSet<PagoFactura>();
        }
    }
}
