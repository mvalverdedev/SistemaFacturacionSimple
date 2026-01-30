using AutoMapper;
using NSubstitute;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Facturas.Commands.CrearFactura;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace APIFacturacion.Tests.Features.Facturas.Commands.CrearFactura
{
    public class CrearFacturaHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositorioAsync<Factura> _facturaRepository;

        public CrearFacturaHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _facturaRepository = Substitute.For<IRepositorioAsync<Factura>>();
            
            _unitOfWork.Repository<Factura>().Returns(_facturaRepository);
        }

        [Fact]
        public async Task Handle_DebeCrearFacturaConDetallesYPagos()
        {
            var comando = new CrearFacturaCommand
            {
                NumeroFactura = "FACT-001",
                IdCliente = 1,
                IdUsuario = 1,
                Total = 150.00m,
                Detalles = new List<DetalleFacturaDto>
                {
                    new DetalleFacturaDto
                    {
                        IdProducto = 1,
                        Cantidad = 2,
                        PrecioUnitario = 50.00m,
                        SubTotal = 100.00m
                    },
                    new DetalleFacturaDto
                    {
                        IdProducto = 2,
                        Cantidad = 1,
                        PrecioUnitario = 50.00m,
                        SubTotal = 50.00m
                    }
                },
                Pagos = new List<PagoFacturaDto>
                {
                    new PagoFacturaDto
                    {
                        IdMetodoPago = 1,
                        Monto = 150.00m
                    }
                }
            };

            var facturaCreada = new Factura();
            _facturaRepository.AgregarAsync(Arg.Any<Factura>()).Returns(Task.FromResult(facturaCreada));
            facturaCreada.Id = 1;

            var handler = new CrearFacturaHandler(_unitOfWork, _mapper);

            var resultado = await handler.Handle(comando, CancellationToken.None);

            Assert.True(resultado.Succeeded);
            Assert.Equal(1, resultado.Datos);
            Assert.Equal("Factura creada exitosamente", resultado.Mensaje);

            await _facturaRepository.Received(1).AgregarAsync(Arg.Is<Factura>(f => 
                f.NumeroFactura == "FACT-001" &&
                f.IdCliente == 1 &&
                f.IdUsuario == 1 &&
                f.Total == 150.00m &&
                f.Detalles.Count == 2 &&
                f.Pagos.Count == 1
            ));
            await _unitOfWork.Received(1).SaveAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_DebeEstablecerFechasCorrectamente()
        {
            var comando = new CrearFacturaCommand
            {
                NumeroFactura = "FACT-002",
                IdCliente = 1,
                IdUsuario = 1,
                Total = 100.00m,
                Detalles = new List<DetalleFacturaDto>
                {
                    new DetalleFacturaDto
                    {
                        IdProducto = 1,
                        Cantidad = 1,
                        PrecioUnitario = 100.00m,
                        SubTotal = 100.00m
                    }
                },
                Pagos = new List<PagoFacturaDto>
                {
                    new PagoFacturaDto
                    {
                        IdMetodoPago = 1,
                        Monto = 100.00m
                    }
                }
            };

            Factura facturaCapturada = null;
            _facturaRepository.AgregarAsync(Arg.Do<Factura>(f => facturaCapturada = f))
                .Returns(callInfo => Task.FromResult(callInfo.Arg<Factura>()));
            
            var handler = new CrearFacturaHandler(_unitOfWork, _mapper);

            await handler.Handle(comando, CancellationToken.None);

            Assert.NotNull(facturaCapturada);
            Assert.True(facturaCapturada.FechaCreacion != default);
            Assert.All(facturaCapturada.Detalles, d => Assert.True(d.FechaCreacion != default));
            Assert.All(facturaCapturada.Pagos, p => 
            {
                Assert.True(p.FechaCreacion != default);
                Assert.True(p.FechaPago != default);
            });
        }
    }
}
