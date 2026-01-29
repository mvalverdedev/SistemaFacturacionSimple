using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace APIFacturacion.Application.Common.Exceptions
{
    public class ExcepcionValidacion : Exception
    {
        public List<string> Errores { get; }

        public ExcepcionValidacion() : base("Se han producido uno o mÃ¡s errores de validaciÃ³n.")
        {
            Errores = new List<string>();
        }

        public ExcepcionValidacion(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errores.Add(failure.ErrorMessage);
            }
        }
    }
}

