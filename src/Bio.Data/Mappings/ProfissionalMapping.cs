using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class ProfissionalMapping : IEntityTypeConfiguration<Profissional>
    {
        public void Configure(EntityTypeBuilder<Profissional> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Especialidade)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.ToTable("Profissionais");
        }
    }
}
