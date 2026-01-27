using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasIndex(p => p.Codigo)
                .IsUnique();
        }
    }
}
