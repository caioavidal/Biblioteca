using Biblioteca.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Configurations
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");

            builder.
                HasKey(x => x.Cod);
            builder.Property(x => x.Titulo);
            builder.Property(x => x.AnoPublicacao);
            builder.Property(x => x.Edicao);
            builder.Property(x => x.Editora);

            builder.HasMany(x => x.Autores).WithMany(x => x.Livros)
                .UsingEntity("LivroAutor",
                l => l.HasOne(typeof(Autor)).WithMany().HasForeignKey("AutorId").HasPrincipalKey(nameof(Autor.Id)),
                r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId").HasPrincipalKey(nameof(Livro.Cod)),
                j => j.HasKey("LivroId", "AutorId"));

            builder.HasMany(x => x.Assuntos).WithMany()
                .UsingEntity("LivroAssunto",
                l => l.HasOne(typeof(Assunto)).WithMany().HasForeignKey("AssuntoId").HasPrincipalKey(nameof(Assunto.Id)),
                r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId").HasPrincipalKey(nameof(Livro.Cod)),
                j => j.HasKey("LivroId", "AssuntoId"));

            builder.HasMany(x => x.Precos).WithOne(x => x.Livro);
        }
    }
}
