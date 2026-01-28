namespace SistemaFacturacion.Domain.Common
{
    public abstract class EntidadBase
    {
        public int Id { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
