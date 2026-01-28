using MediatR;
using SistemaFacturacion.Application.Wrappers;

namespace SistemaFacturacion.Application.Features.Clientes.Commands.ActualizarCliente
{
    public class ActualizarClienteCommand : IRequest<Respuesta<int>>
    {
        public int Id { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
    }
}
