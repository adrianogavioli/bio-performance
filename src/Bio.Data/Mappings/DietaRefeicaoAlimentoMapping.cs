using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class DietaRefeicaoAlimentoMapping : IEntityTypeConfiguration<DietaRefeicaoAlimento>
    {
        public void Configure(EntityTypeBuilder<DietaRefeicaoAlimento> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Quantidade)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(d => d.Observacao)
                .HasColumnType("varchar(1000)");

            builder.HasOne(d => d.Dieta)
                .WithMany(x => x.Refeicoes)
                .HasForeignKey(d => d.DietaId);

            builder.HasOne(d => d.Refeicao)
                .WithMany(r => r.Alimentos)
                .HasForeignKey(d => d.RefeicaoId);

            builder.HasOne(d => d.Alimento)
                .WithMany(a => a.DietasRefeicoesAlimentos)
                .HasForeignKey(d => d.AlimentoId);

            builder.ToTable("DietasRefeicoesAlimentos");
        }
    }
}
