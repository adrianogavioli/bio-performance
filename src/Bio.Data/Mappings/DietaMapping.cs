using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class DietaMapping : IEntityTypeConfiguration<Dieta>
    {
        public void Configure(EntityTypeBuilder<Dieta> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(d => d.TaxaMetabolicaBasal)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(d => d.Observacao)
                .HasColumnType("varchar(1000)");

            builder.Property(d => d.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.HasOne(d => d.Atendimento)
                .WithMany(a => a.Dietas)
                .HasForeignKey(d => d.AtendimentoId);

            builder.ToTable("Dietas");
        }
    }
}
