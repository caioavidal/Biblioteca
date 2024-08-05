namespace Biblioteca.Core.Entities
{
    public class LivroPreco
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public decimal Preco { get; set; }
        public FormaCompra FormaCompra { get; set; }
    }

    public enum FormaCompra
    {
        Balcao = 1,
        SelfService = 2,
        Internet = 3
    }
}
