using SistemaFacturacion.Application;
using SistemaFacturacion.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AgregarCapaAplicacion();
builder.Services.AgregarInfraestructura(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
