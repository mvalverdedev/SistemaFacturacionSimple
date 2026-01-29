namespace APIFacturacion.Application.DTOs
{
    public class ProductoParametros : ParametrosPaginacion
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public bool? SoloConStock { get; set; }
    }
}

