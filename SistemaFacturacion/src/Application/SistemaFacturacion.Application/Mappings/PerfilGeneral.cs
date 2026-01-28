using AutoMapper;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Application.Mappings
{
    public class PerfilGeneral : Profile
    {
        public PerfilGeneral()
        {
            #region DTOs
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Factura, FacturaDto>();
            CreateMap<Producto, ProductoDto>();
            CreateMap<Usuario, UsuarioDto>();

            // Mapeo para factura con detalle completo
            CreateMap<Factura, FacturaDetalleDto>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.NombreRazonSocial))
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.NombreCompleto));

            // Mapeo para detalle de factura con nombre de producto
            CreateMap<DetalleFactura, DetalleFacturaDto>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Producto.Nombre));

            CreateMap<MetodoPago, MetodoPagoDto>();
            #endregion

            #region Commands
            CreateMap<Features.Clientes.Commands.CrearCliente.CrearClienteComando, Cliente>();
            CreateMap<Features.Usuarios.Commands.CrearUsuario.CrearUsuarioCommand, Usuario>();
            CreateMap<Features.Productos.Commands.CrearProducto.CrearProductoCommand, Producto>();
            #endregion
        }
    }
}
