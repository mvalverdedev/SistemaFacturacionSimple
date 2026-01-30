using FluentValidation;

namespace APIFacturacion.Application.Features.Clientes.Commands.CrearCliente
{
    public class CrearClienteValidador : AbstractValidator<CrearClienteComando>
    {
        public CrearClienteValidador()
        {
            RuleFor(p => p.NombreRazonSocial)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de 100 caracteres.");

            RuleFor(p => p.Identificacion)
                .NotEmpty().WithMessage("{PropertyName} es requerido.")
                .MaximumLength(20).WithMessage("{PropertyName} no debe exceder de 20 caracteres.");
            
            RuleFor(p => p.Correo)
                .EmailAddress().WithMessage("{PropertyName} debe ser una direcciÃ³n de correo vÃ¡lida.")
                .When(p => !string.IsNullOrEmpty(p.Correo));
        }
    }
}

