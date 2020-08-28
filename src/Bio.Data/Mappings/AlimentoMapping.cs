using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class AlimentoMapping : IEntityTypeConfiguration<Alimento>
    {
        public void Configure(EntityTypeBuilder<Alimento> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(a => a.Carboidratos)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(a => a.Proteinas)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(a => a.Gorduras)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(a => a.Calorias)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(a => a.Porcao)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.HasOne(a => a.UnidadeMedida)
                .WithMany(u => u.Alimentos)
                .HasForeignKey(a => a.UnidadeMedidaId);

            builder.HasOne(a => a.GrupoAlimento)
                .WithMany(g => g.Alimentos)
                .HasForeignKey(a => a.GrupoAlimentoId);

            builder.ToTable("Alimentos");
        }
    }
}
