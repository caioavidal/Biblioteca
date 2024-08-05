using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Commands
{
    public record GetAutoresQuery : IRequest<IEnumerable<Autor>>;

    public class GetAutoresQueryHandler(IBaseRepository<Autor> autorRepository) : IRequestHandler<GetAutoresQuery, IEnumerable<Autor>>
    {
        public Task<IEnumerable<Autor>> Handle(GetAutoresQuery request, CancellationToken cancellationToken)
        {
            return autorRepository.GetAllAsync();
        }
    }
}
