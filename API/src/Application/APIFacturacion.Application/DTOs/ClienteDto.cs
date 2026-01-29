namespace APIFacturacion.Application.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
    }
}

