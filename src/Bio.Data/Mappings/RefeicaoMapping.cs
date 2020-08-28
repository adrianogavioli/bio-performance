using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class RefeicaoMapping : IEntityTypeConfiguration<Refeicao>
    {
        public void Configure(EntityTypeBuilder<Refeicao> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Ordem)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(r => r.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(r => r.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.ToTable("Refeicoes");
        }
    }
}
