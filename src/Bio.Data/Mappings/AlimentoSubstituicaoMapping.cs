using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class AlimentoSubstituicaoMapping : IEntityTypeConfiguration<AlimentoSubstituicao>
    {
        public void Configure(EntityTypeBuilder<AlimentoSubstituicao> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Alimento)
                .WithMany(x => x.AlimentosSubstituicoes)
                .HasForeignKey(a => a.AlimentoId);

            builder.HasOne(a => a.AlimentoSubstituto)
                .WithMany(x => x.AlimentosSubstitutos)
                .HasForeignKey(a => a.AlimentoSubstitutoId);

            builder.ToTable("AlimentosSubstituicoes");
        }
    }
}
