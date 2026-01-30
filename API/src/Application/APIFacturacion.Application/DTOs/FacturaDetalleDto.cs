using System;
using System.Collections.Generic;

namespace APIFacturacion.Application.DTOs
{
    public class FacturaDetalleDto : FacturaDto
    {
        public ICollection<DetalleFacturaDto> Detalles { get; set; }
        public ICollection<PagoFacturaResponseDto> Pagos { get; set; }
    }
}
