using MediatR;
using SistemaFacturacion.Application.Wrappers;

namespace SistemaFacturacion.Application.Features.Clientes.Commands.CrearCliente
{
    public class CrearClienteComando : IRequest<Respuesta<int>>
    {
        public string NombreRazonSocial { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
    }
}
