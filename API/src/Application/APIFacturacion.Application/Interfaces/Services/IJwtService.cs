using APIFacturacion.Domain.Entities;

namespace APIFacturacion.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(Usuario usuario);
    }
}

