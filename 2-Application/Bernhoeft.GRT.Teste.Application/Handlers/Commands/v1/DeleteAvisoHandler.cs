using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class DeleteAvisoHandler : IRequestHandler<DeleteAvisoRequest, IOperationResult<object>>
    {
        private readonly IServiceProvider _serviceProvider;

        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public DeleteAvisoHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<object>> Handle(DeleteAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.ObterAvisoPorIdAsync(request.Id, TrackingBehavior.Default, cancellationToken);

            if (aviso is null)
                return OperationResult<object>.ReturnNotFound();

            // Soft Delete: apenas marcamos como inativo
            aviso.Ativo = false;
            aviso.DataAlteracao = DateTime.UtcNow;

            _avisoRepository.Update(aviso);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<object>.ReturnNoContent();
        }
    }
}