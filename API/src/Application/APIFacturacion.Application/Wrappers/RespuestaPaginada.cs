using System;
using System.Collections.Generic;

namespace APIFacturacion.Application.Wrappers
{
    public class RespuestaPaginada<T> : Respuesta<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }

        public RespuestaPaginada(T data, int pageNumber, int pageSize, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            Datos = data;
            Succeeded = true;
            Mensaje = null;
            Errores = null;
        }
    }
}

