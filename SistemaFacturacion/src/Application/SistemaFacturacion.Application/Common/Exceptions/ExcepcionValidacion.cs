using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace SistemaFacturacion.Application.Common.Exceptions
{
    public class ExcepcionValidacion : Exception
    {
        public List<string> Errores { get; }

        public ExcepcionValidacion() : base("Se han producido uno o más errores de validación.")
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
