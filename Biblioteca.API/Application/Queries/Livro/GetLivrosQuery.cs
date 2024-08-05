using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Queries
{
    public record GetLivrosQuery : IRequest<IEnumerable<Livro>>;

    public class GetLivrosQueryHandler(IBaseRepository<Livro> livroRepository) : IRequestHandler<GetLivrosQuery, IEnumerable<Livro>>
    {
        public Task<IEnumerable<Livro>> Handle(GetLivrosQuery request, CancellationToken cancellationToken)
        {
            return livroRepository.GetAllAsync();
        }
    }
}
