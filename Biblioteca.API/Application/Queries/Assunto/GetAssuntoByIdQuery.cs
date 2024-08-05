using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Queries
{
    public record GetAssuntoByIdQuery(int Cod) : IRequest<Assunto>;

    public class GetAssuntoByIdQueryHandler(IBaseRepository<Assunto> assuntoRepository) : IRequestHandler<GetAssuntoByIdQuery, Assunto>
    {
        public Task<Assunto> Handle(GetAssuntoByIdQuery request, CancellationToken cancellationToken)
        {
            return assuntoRepository.GetByIdAsync(request.Cod);
        }
    }
}
