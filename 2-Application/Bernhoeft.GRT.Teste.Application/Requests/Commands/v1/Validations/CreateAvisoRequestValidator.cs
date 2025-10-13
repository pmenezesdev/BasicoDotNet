using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class CreateAvisoRequestValidator : AbstractValidator<CreateAvisoRequest>
    {
        public CreateAvisoRequestValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("O título não pode ser vazio.")
                .MaximumLength(50)
                .WithMessage("O título não pode ter mais de 50 caracteres.");

            RuleFor(x => x.Mensagem)
                .NotEmpty()
                .WithMessage("A mensagem não pode ser vazia.");
        }
    }
}