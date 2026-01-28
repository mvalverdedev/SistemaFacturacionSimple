using MediatR;
using SistemaFacturacion.Application.Wrappers;

namespace SistemaFacturacion.Application.Features.Productos.Commands.ActualizarProducto
{
    public class ActualizarProductoCommand : IRequest<Respuesta<int>>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }
}
