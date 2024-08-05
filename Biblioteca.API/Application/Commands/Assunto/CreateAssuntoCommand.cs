using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Commands
{
    public record CreateAssuntoCommand(string Descricao) : IRequest;

    public class CreateAssuntoCommandHandler(IBaseRepository<Assunto> assuntoRepository) : IRequestHandler<CreateAssuntoCommand>
    {
        public async Task Handle(CreateAssuntoCommand request, CancellationToken cancellationToken)
        {
            await assuntoRepository.AddAsync(new Assunto
            {
                Descricao = request.Descricao
            });

            await assuntoRepository.SaveChangesAsync();
        }
    }
}
