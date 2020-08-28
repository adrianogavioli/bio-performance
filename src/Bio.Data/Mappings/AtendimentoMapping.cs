using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class AtendimentoMapping : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(a => a.Objetivo)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(a => a.RestricaoMedica)
                .HasColumnType("varchar(1000)");

            builder.Property(a => a.RestricaoAlimentar)
                .HasColumnType("varchar(1000)");

            builder.Property(a => a.Observacao)
                .HasColumnType("varchar(1000)");

            builder.HasOne(a => a.Profissional)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(a => a.ProfissionalId);

            builder.HasOne(a => a.Paciente)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(a => a.PacienteId);

            builder.ToTable("Atendimentos");
        }
    }
}
