namespace SistemaFacturacion.Application.DTOs
{
    public class ParametrosPaginacion
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool TraerTodo { get; set; }

        public ParametrosPaginacion()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public ParametrosPaginacion(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }
}
