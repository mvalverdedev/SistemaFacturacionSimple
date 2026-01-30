using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APIFacturacion.Domain.Entities;

namespace APIFacturacion.Infrastructure.Persistence.Configurations
{
    public class PagoFacturaConfiguration : IEntityTypeConfiguration<PagoFactura>
    {
        public void Configure(EntityTypeBuilder<PagoFactura> builder)
        {
            builder.ToTable("PagosFactura");

            builder.Ignore(p => p.Activo);

            builder.HasOne(p => p.MetodoPago)
                .WithMany()
                .HasForeignKey(p => p.IdMetodoPago)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Monto)
                .HasColumnType("decimal(18,2)");
        }
    }
}
