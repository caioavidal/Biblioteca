using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Application.Commands
{
    public record UpdateLivroCommand(int Cod, string Titulo, string Editora, int Edicao, string AnoPublicacao, List<Autor> Autores, List<Assunto> Assuntos, List<LivroPreco> Precos) : IRequest<OperationResult>;
    public class UpdateLivroCommandHandler(IBaseRepository<Core.Entities.Livro> livroRepository, IBaseRepository<Autor> autorRepository, IBaseRepository<Assunto> assuntoRepository) : IRequestHandler<UpdateLivroCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(UpdateLivroCommand request, CancellationToken cancellationToken)
        {
            var livro = await livroRepository
                .GetQueryable()
                .Include(x => x.Autores)
                .Include(x => x.Assuntos)
                .Include(x => x.Precos)
                .FirstOrDefaultAsync(x => x.Cod == request.Cod);

            if (livro is null) return OperationResult.Fail(ErrorCode.NotFound);

            var autoresId = request.Autores?.Select(x => x.Id) ?? Enumerable.Empty<int>();

            var autores = await autorRepository
                .GetQueryable()
                .Where(a => autoresId.Contains(a.Id))
                .ToListAsync();

            var assuntosId = request.Assuntos?.Select(x => x.Id) ?? Enumerable.Empty<int>();

            var assuntos = await assuntoRepository
                .GetQueryable()
                .Where(a => assuntosId.Contains(a.Id))
                .ToListAsync();

            livro.AnoPublicacao = request.AnoPublicacao;
            livro.Edicao = request.Edicao;
            livro.Editora = request.Editora;
            livro.Titulo = request.Titulo;

            livro.Autores = autores;
            livro.Assuntos = assuntos;

            foreach (var preco in request.Precos)
            {
                livro.SetPreco(preco.Preco, preco.FormaCompra);
            }

            livroRepository.Update(livro);

            await livroRepository.SaveChangesAsync();

            return OperationResult.Success;
        }
    }
}
