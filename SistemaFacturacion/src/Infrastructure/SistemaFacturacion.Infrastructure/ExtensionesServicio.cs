using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaFacturacion.Domain.Interfaces;
using SistemaFacturacion.Infrastructure.Persistence;
using SistemaFacturacion.Infrastructure.Repositories;

namespace SistemaFacturacion.Infrastructure
{
    public static class ExtensionesServicio
    {
        public static void AgregarInfraestructura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextoAplicacion>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ConexionPorDefecto"),
                    b => b.MigrationsAssembly(typeof(ContextoAplicacion).Assembly.FullName)));

            #region Repositorios
            services.AddTransient(typeof(IRepositorioAsync<>), typeof(RepositorioAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Queries
            services.AddTransient<SistemaFacturacion.Application.Interfaces.Queries.IClienteQuery, SistemaFacturacion.Infrastructure.Queries.ClienteQuery>();
            services.AddTransient<SistemaFacturacion.Application.Interfaces.Queries.IFacturaQuery, SistemaFacturacion.Infrastructure.Queries.FacturaQuery>();
            services.AddTransient<SistemaFacturacion.Application.Interfaces.Queries.IProductoQuery, SistemaFacturacion.Infrastructure.Queries.ProductoQuery>();
            services.AddTransient<SistemaFacturacion.Application.Interfaces.Queries.IUsuarioQuery, SistemaFacturacion.Infrastructure.Queries.UsuarioQuery>();
            #endregion
        }
    }
}
