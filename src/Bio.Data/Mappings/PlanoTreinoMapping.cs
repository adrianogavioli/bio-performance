using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class PlanoTreinoMapping : IEntityTypeConfiguration<PlanoTreino>
    {
        public void Configure(EntityTypeBuilder<PlanoTreino> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.Observacao)
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.HasOne(p => p.Atendimento)
                .WithMany(a => a.PlanosTreinos)
                .HasForeignKey(p => p.AtendimentoId);

            builder.ToTable("PlanosTreinos");
        }
    }
}
