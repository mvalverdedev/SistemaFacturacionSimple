using System;

namespace SistemaFacturacion.Application.DTOs
{
    public class FacturaDto
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
