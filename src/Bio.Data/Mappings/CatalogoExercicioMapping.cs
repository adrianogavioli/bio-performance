using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class CatalogoExercicioMapping : IEntityTypeConfiguration<CatalogoExercicio>
    {
        public void Configure(EntityTypeBuilder<CatalogoExercicio> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(c => c.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.HasOne(c => c.GrupoMuscular)
                .WithMany(g => g.CatalogoExercicios)
                .HasForeignKey(c => c.GrupoMuscularId);

            builder.ToTable("CatalogoExercicios");
        }
    }
}
