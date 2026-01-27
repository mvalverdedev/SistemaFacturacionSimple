using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasIndex(u => u.NombreUsuario)
                .IsUnique();
        }
    }
}
