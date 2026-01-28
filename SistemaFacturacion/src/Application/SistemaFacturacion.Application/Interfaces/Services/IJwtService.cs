using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(Usuario usuario);
    }
}
