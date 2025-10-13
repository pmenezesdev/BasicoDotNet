using Bernhoeft.GRT.Core.Interfaces.Results;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class UpdateAvisoRequest : IRequest<IOperationResult<object>>
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
    }
}