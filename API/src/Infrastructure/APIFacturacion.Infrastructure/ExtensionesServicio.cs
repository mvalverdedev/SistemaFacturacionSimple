using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APIFacturacion.Domain.Interfaces;
using APIFacturacion.Infrastructure.Persistence;
using APIFacturacion.Infrastructure.Repositories;

namespace APIFacturacion.Infrastructure
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
            services.AddTransient<APIFacturacion.Application.Interfaces.Queries.IClienteQuery, APIFacturacion.Infrastructure.Queries.ClienteQuery>();
            services.AddTransient<APIFacturacion.Application.Interfaces.Queries.IFacturaQuery, APIFacturacion.Infrastructure.Queries.FacturaQuery>();
            services.AddTransient<APIFacturacion.Application.Interfaces.Queries.IProductoQuery, APIFacturacion.Infrastructure.Queries.ProductoQuery>();
            services.AddTransient<APIFacturacion.Application.Interfaces.Queries.IUsuarioQuery, APIFacturacion.Infrastructure.Queries.UsuarioQuery>();
            #endregion

            services.AddTransient<APIFacturacion.Application.Interfaces.Services.IJwtService, APIFacturacion.Infrastructure.Services.JwtService>();
        }
    }
}

