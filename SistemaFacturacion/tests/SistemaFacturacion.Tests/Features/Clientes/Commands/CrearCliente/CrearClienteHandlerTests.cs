using AutoMapper;
using NSubstitute;
using SistemaFacturacion.Application.Features.Clientes.Commands.CrearCliente;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SistemaFacturacion.Tests.Features.Clientes.Commands.CrearCliente
{
    public class CrearClienteHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepositorioAsync<Cliente> _clienteRepository;

        public CrearClienteHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _clienteRepository = Substitute.For<IRepositorioAsync<Cliente>>();
            
            _unitOfWork.Repository<Cliente>().Returns(_clienteRepository);
        }

        [Fact]
        public async Task Handle_DebeCrearClienteCorrectamente()
        {
            // Arrange
            var comando = new CrearClienteComando
            {
                NombreRazonSocial = "Cliente Test",
                Identificacion = "1234567890",
                Correo = "test@cliente.com"
            };

            var clienteEntidad = new Cliente
            {
                NombreRazonSocial = "Cliente Test",
                Identificacion = "1234567890",
                Correo = "test@cliente.com"
            };

            // Setup Mapper to return our entity
            _mapper.Map<Cliente>(comando).Returns(clienteEntidad);

            // Setup Repository to return the entity with an ID (simulating DB save)
            _clienteRepository.AgregarAsync(clienteEntidad).Returns(Task.FromResult(clienteEntidad));
            clienteEntidad.Id = 1; // Simulate ID generation

            var handler = new CrearClienteHandler(_unitOfWork, _mapper);

            // Act
            var resultado = await handler.Handle(comando, CancellationToken.None);

            // Assert
            Assert.True(resultado.Succeeded);
            Assert.Equal(1, resultado.Datos);
            Assert.Equal("Cliente creado exitosamente", resultado.Mensaje);
            Assert.True(clienteEntidad.Activo); // Verify the handler set Activo = true

            // Verify interactions
            _mapper.Received(1).Map<Cliente>(comando);
            await _clienteRepository.Received(1).AgregarAsync(clienteEntidad);
            await _unitOfWork.Received(1).SaveAsync(Arg.Any<CancellationToken>());
        }
    }
}
