namespace SistemaFacturacion.Application.DTOs
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
    }
}
