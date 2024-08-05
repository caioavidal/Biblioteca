using Biblioteca.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Configurations
{
    public class AssuntoConfiguration : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> builder)
        {
            builder.ToTable("Assunto");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("CodAs");

            builder.Property(x => x.Descricao);
        }
    }
}
