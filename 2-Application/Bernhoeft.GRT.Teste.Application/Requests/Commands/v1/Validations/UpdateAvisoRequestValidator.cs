using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class UpdateAvisoRequestValidator : AbstractValidator<UpdateAvisoRequest>
    {
        public UpdateAvisoRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID deve ser maior que zero.");

            RuleFor(x => x.Mensagem)
                .NotEmpty()
                .WithMessage("A mensagem não pode ser vazia.");
        }
    }
}