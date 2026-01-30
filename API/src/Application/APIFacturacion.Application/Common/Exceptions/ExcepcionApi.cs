using System;
using System.Globalization;

namespace APIFacturacion.Application.Common.Exceptions
{
    public class ExcepcionApi : Exception
    {
        public ExcepcionApi() : base() { }

        public ExcepcionApi(string message) : base(message) { }

        public ExcepcionApi(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}

