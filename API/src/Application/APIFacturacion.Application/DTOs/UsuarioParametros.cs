namespace APIFacturacion.Application.DTOs
{
    public class UsuarioParametros : ParametrosPaginacion
    {
        public string? NombreUsuario { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Rol { get; set; }
    }
}

