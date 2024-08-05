using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Queries
{
    public record GetAssuntosQuery : IRequest<IEnumerable<Assunto>>;

    public class GetAssuntosQueryHandler(IBaseRepository<Assunto> assuntoRepository) : IRequestHandler<GetAssuntosQuery, IEnumerable<Assunto>>
    {
        public Task<IEnumerable<Assunto>> Handle(GetAssuntosQuery request, CancellationToken cancellationToken)
        {
            return assuntoRepository.GetAllAsync();
        }
    }
}
