using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations
{
    public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            // Configuracion de la tabla
            builder.ToTable("Facturas");

            // Configuracion de relaciones con nombres de columnas especificos
            builder.HasOne(f => f.Cliente)
                .WithMany()
                .HasForeignKey(f => f.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Usuario)
                .WithMany()
                .HasForeignKey(f => f.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuracion de la coleccion de detalles
            builder.HasMany(f => f.Detalles)
                .WithOne(d => d.Factura)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuracion de la coleccion de pagos
            builder.HasMany(f => f.Pagos)
                .WithOne(p => p.Factura)
                .HasForeignKey(p => p.IdFactura)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
