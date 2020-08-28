using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class GrupoMuscularMapping : IEntityTypeConfiguration<GrupoMuscular>
    {
        public void Configure(EntityTypeBuilder<GrupoMuscular> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Descricao)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(g => g.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.ToTable("GruposMusculares");
        }
    }
}
