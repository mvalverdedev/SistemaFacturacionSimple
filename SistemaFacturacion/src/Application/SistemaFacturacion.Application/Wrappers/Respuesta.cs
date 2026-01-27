using System.Collections.Generic;

namespace SistemaFacturacion.Application.Wrappers
{
    public class Respuesta<T>
    {
        public bool Succeeded { get; set; }
        public string Mensaje { get; set; }
        public List<string> Errores { get; set; }
        public T Datos { get; set; }

        public Respuesta()
        {
        }

        public Respuesta(T data, string message = null)
        {
            Succeeded = true;
            Mensaje = message;
            Datos = data;
        }

        public Respuesta(string message)
        {
            Succeeded = false;
            Mensaje = message;
        }
    }
}
