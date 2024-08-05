using Biblioteca.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Configurations
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Cod");

            builder.Property(x => x.Nome);

            builder.HasMany(x => x.Livros).WithMany(x => x.Autores)
              .UsingEntity("LivroAutor",
              l => l.HasOne(typeof(Autor)).WithMany().HasForeignKey("AutorId").HasPrincipalKey(nameof(Autor.Id)),
              r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId").HasPrincipalKey(nameof(Livro.Cod)),
              j => j.HasKey("LivroId", "AutorId"));
        }
    }
}
