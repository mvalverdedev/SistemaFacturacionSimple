using AutoMapper;
using NSubstitute;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Infrastructure.Queries;
using APIFacturacion.Tests.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace APIFacturacion.Tests.Features.Facturas.Queries
{
    public class FacturaQueryTests : TestBase
    {
        [Fact]
        public async Task DebeFiltrarPorTotal()
        {
            // Arrange
            var contexto = ObtenerContexto();
            contexto.Facturas.Add(new Factura { NumeroFactura = "F001", Total = 100, FechaCreacion = DateTime.Now });
            contexto.Facturas.Add(new Factura { NumeroFactura = "F002", Total = 200, FechaCreacion = DateTime.Now });
            await contexto.SaveChangesAsync();

            var mapperMock = Substitute.For<IMapper>();
            mapperMock.ConfigurationProvider.Returns(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Factura, FacturaDto>();
            }));

            var query = new FacturaQuery(contexto, mapperMock);
            var request = new ObtenerFacturasPaginadasQuery { Total = 200, PageNumber = 1, PageSize = 10 };

            // Act
            var result = await query.ObtenerFacturasPaginadasAsync(request);

            // Assert
            Assert.Single(result);
            Assert.Equal(200, result[0].Total);
        }
    }
}

