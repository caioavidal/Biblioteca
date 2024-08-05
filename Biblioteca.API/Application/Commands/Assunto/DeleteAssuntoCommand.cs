using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Application.Commands
{
    public record DeleteAssuntoCommand(int Id) : IRequest<OperationResult>;

    public class DeleteAssuntoCommandHandler(IBaseRepository<Assunto> assuntoRepository, IBaseRepository<Livro> livroRepository) : IRequestHandler<DeleteAssuntoCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(DeleteAssuntoCommand request, CancellationToken cancellationToken)
        {
            var hasAnyAssunto = livroRepository
                .GetQueryable()
                .Include(x=>x.Assuntos)
                .Any(x => x.Assuntos.Any(a => a.Id == request.Id));

            if (hasAnyAssunto)
            {
                return OperationResult.Fail(ErrorCode.InternalServerError, "Assunto está vinculado a um livro");
            }

            await assuntoRepository.DeleteAsync(request.Id);

            await assuntoRepository.SaveChangesAsync();

            return OperationResult.Success;
        }
    }
}
