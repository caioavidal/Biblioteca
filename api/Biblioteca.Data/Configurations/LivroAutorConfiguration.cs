using Biblioteca.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Configurations
{
    public class LivroAutorConfiguration : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.ToTable("Livro_Autor");

            builder.HasKey(x => new { x.AutorId, x.LivroId });
            builder.Property(x => x.AutorId).HasColumnName("Autor_CodAu");
            builder.Property(x => x.LivroId).HasColumnName("Livro_CodL");
        }
    }
}
