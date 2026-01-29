using AutoMapper;
using NSubstitute;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Infrastructure.Queries;
using APIFacturacion.Tests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace APIFacturacion.Tests.Features.Clientes.Queries
{
    public class ClienteQueryTests : TestBase
    {
        [Fact]
        public async Task DebeRetornarClientesPaginados()
        {
            // Arrange
            var contexto = ObtenerContexto();
            contexto.Clientes.Add(new Cliente { NombreRazonSocial = "Cliente 1", Identificacion = "111", Activo = true, Telefono = "123", Correo = "c1@test.com", Direccion = "Dir 1" });
            contexto.Clientes.Add(new Cliente { NombreRazonSocial = "Cliente 2", Identificacion = "222", Activo = true, Telefono = "123", Correo = "c2@test.com", Direccion = "Dir 2" });
            contexto.Clientes.Add(new Cliente { NombreRazonSocial = "Cliente 3", Identificacion = "333", Activo = true, Telefono = "123", Correo = "c3@test.com", Direccion = "Dir 3" });
            await contexto.SaveChangesAsync();

            var mapperMock = Substitute.For<IMapper>();
            mapperMock.ConfigurationProvider.Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteDto>();
            }));

            var query = new ClienteQuery(contexto, mapperMock);
            var request = new ObtenerClientesPaginadosQuery { PageNumber = 1, PageSize = 2 };

            // Act
            var result = await query.ObtenerClientesPaginadosAsync(request);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DebeFiltrarPorNombre()
        {
             // Arrange
            var contexto = ObtenerContexto();
            contexto.Clientes.Add(new Cliente { NombreRazonSocial = "Juan Perez", Identificacion = "111", Activo = true, Telefono = "123", Correo = "juan@test.com", Direccion = "Dir 1" });
            contexto.Clientes.Add(new Cliente { NombreRazonSocial = "Maria Lopez", Identificacion = "222", Activo = true, Telefono = "123", Correo = "maria@test.com", Direccion = "Dir 2" });
            await contexto.SaveChangesAsync();

            var mapperMock = Substitute.For<IMapper>();
            mapperMock.ConfigurationProvider.Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteDto>();
            }));

            var query = new ClienteQuery(contexto, mapperMock);
            var request = new ObtenerClientesPaginadosQuery { NombreRazonSocial = "Juan", PageNumber = 1, PageSize = 10 };

            // Act
            var result = await query.ObtenerClientesPaginadosAsync(request);

            // Assert
            Assert.Single(result);
            Assert.Equal("Juan Perez", result[0].NombreRazonSocial);
        }
    }
}

