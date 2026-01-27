using System;
using System.Collections.Generic;

namespace SistemaFacturacion.Application.DTOs
{
    public class FacturaDetalleDto : FacturaDto
    {
        public string NombreCliente { get; set; }
        public string NombreUsuario { get; set; }
        public ICollection<DetalleFacturaDto> Detalles { get; set; }
    }
}
