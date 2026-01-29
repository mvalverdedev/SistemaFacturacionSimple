using APIFacturacion.Domain.Entities;
using Xunit;

namespace APIFacturacion.Tests.Domain.Entities
{
    public class ClienteTests
    {
        [Fact]
        public void Cliente_DebeTenerPropiedadesCorrectas()
        {
            // Arrange
            var cliente = new Cliente
            {
                NombreRazonSocial = "Juan Perez",
                Identificacion = "0987654321",
                Telefono = "555-1234",
                Correo = "juan@example.com",
                Direccion = "Av. Siempre Viva 123",
                Activo = true
            };

            // Act & Assert
            Assert.Equal("Juan Perez", cliente.NombreRazonSocial);
            Assert.Equal("0987654321", cliente.Identificacion);
            Assert.Equal("555-1234", cliente.Telefono);
            Assert.Equal("juan@example.com", cliente.Correo);
            Assert.Equal("Av. Siempre Viva 123", cliente.Direccion);
            Assert.True(cliente.Activo);
        }
    }
}

