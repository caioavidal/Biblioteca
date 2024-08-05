namespace Biblioteca.Core.Entities
{
    public class Livro
    {
        public int Cod { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public ICollection<LivroPreco> Precos { get; set; } = new List<LivroPreco>();
        public ICollection<Autor> Autores { get; set; } = new List<Autor>();
        public ICollection<Assunto> Assuntos { get; set; } = new List<Assunto>();

        public void SetPreco(decimal preco, FormaCompra formaCompra)
        {
            var livroPreco = Precos.FirstOrDefault(x => x.FormaCompra == formaCompra);
            if (livroPreco is null)
            {
                Precos.Add(new LivroPreco
                {
                    FormaCompra = formaCompra,
                    LivroId = Cod,
                    Preco = preco
                });

                return;
            }

            livroPreco.Preco = preco;
        }
    }
}
