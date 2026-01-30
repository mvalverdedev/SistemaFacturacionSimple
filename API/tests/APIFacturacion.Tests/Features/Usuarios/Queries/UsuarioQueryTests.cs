using AutoMapper;
using NSubstitute;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Infrastructure.Queries;
using APIFacturacion.Tests.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace APIFacturacion.Tests.Features.Usuarios.Queries
{
    public class UsuarioQueryTests : TestBase
    {
        [Fact]
        public async Task DebeFiltrarPorRol()
        {
            // Arrange
            var contexto = ObtenerContexto();
            contexto.Usuarios.Add(new Usuario { NombreUsuario = "User1", Rol = "Admin", Activo = true, FechaCreacion = DateTime.Now, Clave = "123", NombreCompleto = "Administrador" });
            contexto.Usuarios.Add(new Usuario { NombreUsuario = "User2", Rol = "Vendedor", Activo = true, FechaCreacion = DateTime.Now, Clave = "123", NombreCompleto = "Vendedor 1" });
            await contexto.SaveChangesAsync();

            var mapperMock = Substitute.For<IMapper>();
            mapperMock.ConfigurationProvider.Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioDto>();
            }));

            var query = new UsuarioQuery(contexto, mapperMock);
            var request = new ObtenerUsuariosPaginadosQuery { Rol = "Admin", PageNumber = 1, PageSize = 10 };

            // Act
            var result = await query.ObtenerUsuariosPaginadosAsync(request);

            // Assert
            Assert.Single(result);
            Assert.Equal("User1", result[0].NombreUsuario);
        }
    }
}

