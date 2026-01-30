using AutoMapper;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Domain.Entities;

namespace APIFacturacion.Application.Mappings
{
    public class PerfilGeneral : Profile
    {
        public PerfilGeneral()
        {
            #region DTOs
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Factura, FacturaDto>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.NombreRazonSocial))
                .ForMember(dest => dest.NombreVendedor, opt => opt.MapFrom(src => src.Usuario.NombreCompleto));
            CreateMap<Producto, ProductoDto>();
            CreateMap<Usuario, UsuarioDto>();

            CreateMap<Factura, FacturaDetalleDto>()
                .IncludeBase<Factura, FacturaDto>();

            // Mapeo para detalle de factura con nombre de producto
            CreateMap<DetalleFactura, DetalleFacturaDto>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Producto.Nombre));

            CreateMap<MetodoPago, MetodoPagoDto>();
            CreateMap<PagoFactura, PagoFacturaDto>();
            CreateMap<PagoFactura, PagoFacturaResponseDto>()
                .ForMember(dest => dest.FormaPago, opt => opt.MapFrom(src => src.MetodoPago.Nombre));
            #endregion

            #region Commands
            CreateMap<Features.Clientes.Commands.CrearCliente.CrearClienteComando, Cliente>();
            CreateMap<Features.Usuarios.Commands.CrearUsuario.CrearUsuarioCommand, Usuario>();
            CreateMap<Features.Productos.Commands.CrearProducto.CrearProductoCommand, Producto>();
            CreateMap<DetalleFacturaDto, DetalleFactura>();
            CreateMap<PagoFacturaDto, PagoFactura>();
            #endregion
        }
    }
}

