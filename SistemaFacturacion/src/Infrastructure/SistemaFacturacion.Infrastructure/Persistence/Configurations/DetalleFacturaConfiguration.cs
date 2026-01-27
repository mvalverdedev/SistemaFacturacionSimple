using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations
{
    public class DetalleFacturaConfiguration : IEntityTypeConfiguration<DetalleFactura>
    {
        public void Configure(EntityTypeBuilder<DetalleFactura> builder)
        {
            // Configuracion de la tabla
            builder.ToTable("DetallesFactura");

            // Configuracion de relacion con Producto
            builder.HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuracion de propiedades decimales
            builder.Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.SubTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
