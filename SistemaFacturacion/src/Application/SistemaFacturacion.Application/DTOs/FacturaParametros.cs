using System;

namespace SistemaFacturacion.Application.DTOs
{
    public class FacturaParametros : ParametrosPaginacion
    {
        public string? NumeroFactura { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public decimal? Total { get; set; }
    }
}
