using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Infrastructure.Persistence;
using System;

namespace SistemaFacturacion.Tests.Common
{
    public class TestBase
    {
        public ContextoAplicacion ObtenerContexto()
        {
            var options = new DbContextOptionsBuilder<ContextoAplicacion>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var contexto = new ContextoAplicacion(options);
            return contexto;
        }
    }
}
