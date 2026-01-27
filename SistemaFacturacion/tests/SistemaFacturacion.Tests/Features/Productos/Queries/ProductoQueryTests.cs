using AutoMapper;
using NSubstitute;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Infrastructure.Queries;
using SistemaFacturacion.Tests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SistemaFacturacion.Tests.Features.Productos.Queries
{
    public class ProductoQueryTests : TestBase
    {
        [Fact]
        public async Task DebeFiltrarPorCodigo()
        {
            // Arrange
            var contexto = ObtenerContexto();
            contexto.Productos.Add(new Producto { Codigo = "P001", Nombre = "Prod 1", PrecioUnitario = 10, Stock = 100, Activo = true });
            contexto.Productos.Add(new Producto { Codigo = "P002", Nombre = "Prod 2", PrecioUnitario = 20, Stock = 50, Activo = true });
            await contexto.SaveChangesAsync();

            var mapperMock = Substitute.For<IMapper>();
            mapperMock.ConfigurationProvider.Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Producto, ProductoDto>();
            }));

            var query = new ProductoQuery(contexto, mapperMock);
            var request = new ObtenerProductosPaginadosQuery { Codigo = "P001", PageNumber = 1, PageSize = 10 };

            // Act
            var result = await query.ObtenerProductosPaginadosAsync(request);

            // Assert
            Assert.Single(result);
            Assert.Equal("P001", result[0].Codigo);
        }
    }
}
