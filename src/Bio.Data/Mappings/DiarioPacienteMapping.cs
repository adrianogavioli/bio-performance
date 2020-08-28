using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class DiarioPacienteMapping : IEntityTypeConfiguration<DiarioPaciente>
    {
        public void Configure(EntityTypeBuilder<DiarioPaciente> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(d => d.Peso)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(d => d.Observacao)
                .HasMaxLength(1000);

            builder.HasOne(d => d.Paciente)
                .WithMany(p => p.Diarios)
                .HasForeignKey(d => d.PacienteId);

            builder.HasOne(d => d.Treino)
                .WithMany(t => t.Diarios)
                .HasForeignKey(d => d.TreinoId);

            builder.ToTable("DiariosPacientes");
        }
    }
}
