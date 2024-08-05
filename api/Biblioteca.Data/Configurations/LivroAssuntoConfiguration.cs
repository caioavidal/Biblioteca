using Biblioteca.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Configurations
{
    public class LivroAssuntoConfiguration : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> builder)
        {
            builder.ToTable("Livro_Assunto");

            builder.HasKey(x => new { x.AssuntoId, x.LivroId });

            builder.Property(x => x.AssuntoId).HasColumnName("Assunto_CodAs");

            builder.Property(x => x.LivroId).HasColumnName("Livro_CodL");
        }
    }
}
