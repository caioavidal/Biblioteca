using System.ComponentModel.DataAnnotations;
using Biblioteca.Core.Entities;

namespace Biblioteca.API.DTOs
{
    #region Livro
    public record CreateLivroRequestDto([Required] string Titulo, string Editora, int Edicao, string AnoPublicacao, List<Autor> Autores, List<Assunto> Assuntos, List<LivroPreco> Precos);
    public record UpdateLivroRequestDto([Required] string Titulo, string Editora, int Edicao, string AnoPublicacao, List<Autor> Autores, List<Assunto> Assuntos, List<LivroPreco> Precos);
    public record UpdateLivroPrecoRequestDto([Required] decimal Preco, [Required] FormaCompra FormaCompra);

    #endregion

    #region Autor
    public record CreateAutorRequestDto([Required] string Nome);
    public record UpdateAutorRequestDto([Required] string Nome);
    #endregion

    #region Assunto
    public record CreateAssuntoRequestDto([Required(ErrorMessage = "Campo Descrição é obrigatório.")] string Descricao);
    public record UpdateAssuntoRequestDto([Required] string Descricao);
    #endregion
}

