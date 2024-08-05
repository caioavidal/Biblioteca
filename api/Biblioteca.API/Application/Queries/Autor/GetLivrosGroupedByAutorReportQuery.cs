using Biblioteca.Data;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Application.Queries.Autor
{
    public record LivroReportQueryResult(string Titulo, string Editora, int Edicao, string AnoPublicacao, string Assuntos);
    public record LivroByAutorQueryResult(string Autor, List<LivroReportQueryResult> Livros);
    public record GetLivrosGroupedByAutorReportQuery : IRequest<IEnumerable<LivroByAutorQueryResult>>;

    public class GetLivrosGroupedByAutorReportQueryHandler(BibliotecaDbContext dbContext) : IRequestHandler<GetLivrosGroupedByAutorReportQuery, IEnumerable<LivroByAutorQueryResult>>
    {
        public async Task<IEnumerable<LivroByAutorQueryResult>> Handle(GetLivrosGroupedByAutorReportQuery request, CancellationToken cancellationToken)
        {
            var sql = @"SELECT A.Cod CodAutor, A.Nome, L.Titulo, L.Editora, L.Edicao, L.AnoPublicacao, AST.Descricao, L.Cod CodLivro FROM Autor A
                        LEFT JOIN LivroAutor LA on LA.AutorId = A.Cod
                        LEFT JOIN Livro L on L.Cod = LA.LivroId
                        LEFT JOIN LivroAssunto LAS on LAS.LivroId = L.Cod
                        LEFT JOIN Assunto AST on AST.CodAs = LAS.AssuntoId";

            var result = await dbContext.Database.GetDbConnection().QueryAsync<QueryResult>(sql);

            return result.GroupBy(x => x.CodAutor).Select(x => new LivroByAutorQueryResult
            (
                Autor: x.FirstOrDefault()?.Nome,
                Livros: x.Where(l=>l.CodLivro != 0).GroupBy(l => l.CodLivro).Select(l =>
                {
                    var firstLivro = l.FirstOrDefault();

                    return new LivroReportQueryResult(
                    Titulo: firstLivro.Titulo,
                    AnoPublicacao: firstLivro.AnoPublicacao,
                    Edicao: firstLivro.Edicao,
                    Editora: firstLivro.Editora,
                    Assuntos: string.Join(", ", l.Select(z => z.Descricao).Distinct().Where(z => !string.IsNullOrEmpty(z))));
                }).ToList()
            ));
        }

        internal sealed record QueryResult
        {
            public int CodAutor { get; set; }
            public int Name { get; set; }
            public string Descricao { get; set; }
            public string AnoPublicacao { get; set; }
            public string Titulo { get; set; }
            public string Editora { get; set; }
            public int Edicao { get; set; }
            public string Nome { get; set; }
            public int CodLivro { get; set; }
        }
    }
}
