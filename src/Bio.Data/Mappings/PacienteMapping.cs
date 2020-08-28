using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bio.Data.Mappings
{
    public class PacienteMapping : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.CPF)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(p => p.DataNascimento)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(p => p.Altura)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Genero)
                .IsRequired();

            builder.ToTable("Pacientes");
        }
    }
}
