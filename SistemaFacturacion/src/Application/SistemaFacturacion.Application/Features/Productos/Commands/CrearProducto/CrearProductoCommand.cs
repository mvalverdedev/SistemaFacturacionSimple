using MediatR;
using SistemaFacturacion.Application.Wrappers;

namespace SistemaFacturacion.Application.Features.Productos.Commands.CrearProducto
{
    public class CrearProductoCommand : IRequest<Respuesta<int>>
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }
}
