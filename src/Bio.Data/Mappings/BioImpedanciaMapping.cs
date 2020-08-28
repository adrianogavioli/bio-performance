using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class BioImpedanciaMapping : IEntityTypeConfiguration<BioImpedancia>
    {
        public void Configure(EntityTypeBuilder<BioImpedancia> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(b => b.AguaCorporal)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(b => b.GorduraCorporal)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(b => b.Proteinas)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(b => b.Minerais)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(b => b.TaxaMetabolicaBasal)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(b => b.Paciente)
                .WithMany(p => p.BioImpedancias)
                .HasForeignKey(b => b.PacienteId);

            builder.ToTable("BioImpedancias");
        }
    }
}
