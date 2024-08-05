using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Application.Queries
{
    public record GetLivroByIdQuery(int Cod) : IRequest<Livro>;

    public class GetLivroByIdQueryHandler(IBaseRepository<Livro> livroRepository) : IRequestHandler<GetLivroByIdQuery, Livro>
    {
        public Task<Livro> Handle(GetLivroByIdQuery request, CancellationToken cancellationToken)
        {
            return livroRepository
                .GetQueryable()
                .Include(x => x.Autores)
                .Include(x => x.Assuntos)
                .Include(x => x.Precos)
                .FirstOrDefaultAsync(x => x.Cod == request.Cod);
        }
    }
}
