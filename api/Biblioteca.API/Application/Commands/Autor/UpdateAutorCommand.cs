using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;

namespace Biblioteca.API.Application.Commands
{
    public record UpdateAutorCommand(int Id, string Nome) : IRequest<OperationResult>;
    public class UpdateAutorCommandHandler(IBaseRepository<Autor> autorRepository) : IRequestHandler<UpdateAutorCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(UpdateAutorCommand request, CancellationToken cancellationToken)
        {
            var autor = await autorRepository.GetByIdAsync(request.Id);
            if (autor is null) return OperationResult.Fail(ErrorCode.NotFound);

            autor.Nome = request.Nome;

            autorRepository.Update(autor);

            await autorRepository.SaveChangesAsync();

            return OperationResult.Success;
        }
    }
}
