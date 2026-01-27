namespace SistemaFacturacion.Application.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }
}
