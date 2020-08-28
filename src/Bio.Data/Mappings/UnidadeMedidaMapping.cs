using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class UnidadeMedidaMapping : IEntityTypeConfiguration<UnidadeMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Codigo)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(u => u.Descricao)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(u => u.Ativo)
                .IsRequired()
                .HasColumnType("bit");

            builder.ToTable("UnidadesMedidas");
        }
    }
}
