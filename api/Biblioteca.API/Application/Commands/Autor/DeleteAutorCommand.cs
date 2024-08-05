using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Application.Commands
{
    public record DeleteAutorCommand(int Id) : IRequest<OperationResult>;

    public class DeleteAutorCommandHandler(IBaseRepository<Autor> autorRepository, IBaseRepository<Livro> livroRepository) : IRequestHandler<DeleteAutorCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(DeleteAutorCommand request, CancellationToken cancellationToken)
        {
            var hasAnyLivro = livroRepository
                .GetQueryable()
                .Include(x => x.Autores)
                .Any(x => x.Autores.Any(a => a.Id == request.Id));

            if (hasAnyLivro) return OperationResult.Fail(ErrorCode.InternalServerError, "Autor está vinculado a um livro");

            await autorRepository.DeleteAsync(request.Id);

            await autorRepository.SaveChangesAsync();

            return OperationResult.Success;
        }
    }
}
