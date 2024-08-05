using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Commands
{
    public record CreateAutorCommand(string Nome) : IRequest;

    public class CreateAutorCommandHandler(IBaseRepository<Autor> autorRepository) : IRequestHandler<CreateAutorCommand>
    {
        public async Task Handle(CreateAutorCommand request, CancellationToken cancellationToken)
        {
            await autorRepository.AddAsync(new Autor
            {
                Nome = request.Nome
            });

            await autorRepository.SaveChangesAsync();
        }
    }
}
