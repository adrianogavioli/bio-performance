using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class ExercicioMapping : IEntityTypeConfiguration<Exercicio>
    {
        public void Configure(EntityTypeBuilder<Exercicio> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Ordem)
                .IsRequired()
                .HasColumnType("decimal(3,1)");

            builder.Property(e => e.Repeticao)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Observacao)
                .HasColumnType("varchar(1000)");

            builder.HasOne(e => e.Treino)
                .WithMany(t => t.Exercicios)
                .HasForeignKey(e => e.TreinoId);

            builder.HasOne(e => e.CatalogoExercicio)
                .WithMany(c => c.Exercicios)
                .HasForeignKey(e => e.CatalogoExercicioId);

            builder.ToTable("Exercicios");
        }
    }
}
