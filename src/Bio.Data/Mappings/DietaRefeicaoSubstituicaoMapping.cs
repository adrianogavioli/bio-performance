using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class DietaRefeicaoSubstituicaoMapping : IEntityTypeConfiguration<DietaRefeicaoSubstituicao>
    {
        public void Configure(EntityTypeBuilder<DietaRefeicaoSubstituicao> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Quantidade)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(s => s.Observacao)
                .HasColumnType("varchar(1000)");

            builder.HasOne(s => s.DietaRefeicaoAlimento)
                .WithMany(d => d.DietasRefeicoesSubstituicoes)
                .HasForeignKey(s => s.DietaRefeicaoAlimentoId);

            builder.HasOne(s => s.Alimento)
                .WithMany(a => a.DietasRefeicoesSubstituicoes)
                .HasForeignKey(s => s.AlimentoId);

            builder.ToTable("DietasRefeicoesSubstituicoes");
        }
    }
}
