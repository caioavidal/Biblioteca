using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Commands
{
    public record GetAutorByIdQuery(int Cod) : IRequest<Autor>;

    public class GetAutorByIdQueryHandler(IBaseRepository<Autor> livroRepository) : IRequestHandler<GetAutorByIdQuery, Autor>
    {
        public Task<Autor> Handle(GetAutorByIdQuery request, CancellationToken cancellationToken)
        {
            return livroRepository.GetByIdAsync(request.Cod);
        }
    }
}
