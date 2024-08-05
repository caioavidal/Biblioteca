using Biblioteca.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Configurations
{
    public class LivroPrecoConfiguration : IEntityTypeConfiguration<LivroPreco>
    {
        public void Configure(EntityTypeBuilder<LivroPreco> builder)
        {
            builder.ToTable("LivroPreco");

            builder.
                HasKey(x => new { x.LivroId, x.FormaCompra });

            builder.Property(x => x.Preco);
            builder.Property(x => x.FormaCompra);

            builder.HasOne(x => x.Livro);
        }
    }
}
