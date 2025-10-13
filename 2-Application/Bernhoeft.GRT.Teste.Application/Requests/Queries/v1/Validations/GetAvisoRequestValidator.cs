using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations
{
    public class GetAvisoRequestValidator : AbstractValidator<GetAvisoRequest>
    {
        public GetAvisoRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID deve ser maior que zero.");
        }
    }
}