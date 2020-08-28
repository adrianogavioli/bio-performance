using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class TreinoRelGrupoMuscularMapping : IEntityTypeConfiguration<TreinoRelGrupoMuscular>
    {
        public void Configure(EntityTypeBuilder<TreinoRelGrupoMuscular> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Treino)
                .WithMany(t => t.GruposMusculares)
                .HasForeignKey(r => r.TreinoId);

            builder.HasOne(r => r.GrupoMuscular)
                .WithMany(g => g.Treinos)
                .HasForeignKey(r => r.GrupoMuscularId);

            builder.ToTable("TreinosRelGruposMusculares");
        }
    }
}
