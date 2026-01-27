namespace SistemaFacturacion.Application.DTOs
{
    public class ClienteParametros : ParametrosPaginacion
    {
        public string? NombreRazonSocial { get; set; }
        public string? Identificacion { get; set; }
    }
}
