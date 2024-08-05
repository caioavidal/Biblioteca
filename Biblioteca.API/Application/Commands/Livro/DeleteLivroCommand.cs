using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Commands
{
    public record DeleteLivroCommand(int Cod) : IRequest;
    public class DeleteLivroCommandHandler(IBaseRepository<Livro> livroRepository) : IRequestHandler<DeleteLivroCommand>
    {
        public async Task Handle(DeleteLivroCommand request, CancellationToken cancellationToken)
        {
            await livroRepository.DeleteAsync(request.Cod);

            await livroRepository.SaveChangesAsync();
        }
    }
}
