using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class TreinoMapping : IEntityTypeConfiguration<Treino>
    {
        public void Configure(EntityTypeBuilder<Treino> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Ordem)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(t => t.Titulo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Observacao)
                .HasColumnType("varchar(1000)");

            builder.Property(t => t.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.HasOne(t => t.PlanoTreino)
                .WithMany(p => p.Treinos)
                .HasForeignKey(t => t.PlanoTreinoId);

            builder.ToTable("Treinos");
        }
    }
}
