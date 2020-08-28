using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class GrupoAlimentoMapping : IEntityTypeConfiguration<GrupoAlimento>
    {
        public void Configure(EntityTypeBuilder<GrupoAlimento> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Codigo)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(g => g.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(g => g.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.ToTable("GruposAlimentos");
        }
    }
}
