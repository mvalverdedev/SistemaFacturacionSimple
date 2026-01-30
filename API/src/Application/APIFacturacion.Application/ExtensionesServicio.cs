using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using APIFacturacion.Application.Mappings;

namespace APIFacturacion.Application
{
    public static class ExtensionesServicio
    {
        public static void AgregarCapaAplicacion(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PerfilGeneral));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}

