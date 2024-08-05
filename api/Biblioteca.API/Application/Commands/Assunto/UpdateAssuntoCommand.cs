using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Commands
{
    public record UpdateAssuntoCommand(int Id, string Descricao) : IRequest<OperationResult>;
    public class UpdateAssuntoCommandHandler(IBaseRepository<Assunto> assuntoRepository) : IRequestHandler<UpdateAssuntoCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(UpdateAssuntoCommand request, CancellationToken cancellationToken)
        {
            var assunto = await assuntoRepository.GetByIdAsync(request.Id);
            if (assunto == null) return OperationResult.Fail(ErrorCode.NotFound);

            assunto.Descricao = request.Descricao;

            assuntoRepository.Update(assunto);
            await assuntoRepository.SaveChangesAsync();

            return OperationResult.Success;
        }
    }
}
