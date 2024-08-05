using Biblioteca.Core.Entities;
using Biblioteca.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Application.Commands
{
    public record CreateLivroCommand(string Titulo, string Editora, int Edicao, string AnoPublicacao, List<Autor> Autores, List<Assunto> Assuntos, List<LivroPreco> Precos) : IRequest;

    public class CreateLivroCommandHandler(IBaseRepository<Livro> livroRepository, IBaseRepository<Autor> autorRepository, IBaseRepository<Assunto> assuntoRepository) : IRequestHandler<CreateLivroCommand>
    {
        public async Task Handle(CreateLivroCommand request, CancellationToken cancellationToken)
        {
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

            var livro = new Livro
            {
                AnoPublicacao = request.AnoPublicacao,
                Edicao = request.Edicao,
                Editora = request.Editora,
                Titulo = request.Titulo,
                Autores = autores,
                Assuntos = assuntos
            };

            foreach (var preco in request.Precos)
            {
                livro.SetPreco(preco.Preco, preco.FormaCompra);
            }

            await livroRepository.AddAsync(livro);

            await livroRepository.SaveChangesAsync();
        }
    }
}
