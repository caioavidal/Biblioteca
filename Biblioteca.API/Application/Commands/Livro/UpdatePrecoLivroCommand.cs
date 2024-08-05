using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Application.Commands
{
    public record UpdatePrecoLivroCommand(int LivroId, decimal Preco, FormaCompra FormaCompra) : IRequest<OperationResult>;

    public class UpdatePrecoLivroCommandHandler(IBaseRepository<Livro> repository) : IRequestHandler<UpdatePrecoLivroCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(UpdatePrecoLivroCommand request, CancellationToken cancellationToken)
        {
            var livro = await repository
                .GetQueryable()
                .Include(x => x.Precos)
                .FirstOrDefaultAsync(x => x.Cod == request.LivroId);

            if (livro == null)
            {
                return OperationResult.Fail(ErrorCode.NotFound);
            }

            livro.SetPreco(request.Preco, request.FormaCompra);

            repository.Update(livro);
            await repository.SaveChangesAsync();

            return OperationResult.Success;
        }
    }
}
