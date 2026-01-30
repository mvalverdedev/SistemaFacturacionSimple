using APIFacturacion.Domain.Common;

namespace APIFacturacion.Domain.Entities
{
    public class DetalleFactura : EntidadBase
    {
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }

        // Propiedades de Navegacion
        public virtual Factura Factura { get; set; }
        public virtual Producto Producto { get; set; }
    }
}

