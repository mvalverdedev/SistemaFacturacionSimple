using System;

namespace APIFacturacion.Application.DTOs
{
    public class FacturaDto
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public string NombreCliente { get; set; }
        public string NombreVendedor { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

