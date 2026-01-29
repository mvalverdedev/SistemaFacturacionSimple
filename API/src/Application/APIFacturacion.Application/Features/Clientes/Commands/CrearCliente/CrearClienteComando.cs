using MediatR;
using APIFacturacion.Application.Wrappers;

namespace APIFacturacion.Application.Features.Clientes.Commands.CrearCliente
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

