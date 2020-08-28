using Bio.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bio.Data.Context
{
    public class BioDbContext : DbContext
    {
        public BioDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<AlimentoSubstituicao> AlimentosSubstituicoes { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<BioImpedancia> BioImpedancias { get; set; }
        public DbSet<CatalogoExercicio> CatalogoExercicios { get; set; }
        public DbSet<DiarioPaciente> DiariosPacientes { get; set; }
        public DbSet<Dieta> Dietas { get; set; }
        public DbSet<DietaRefeicaoAlimento> DietasRefeicoesAlimentos { get; set; }
        public DbSet<DietaRefeicaoSubstituicao> DietasRefeicoesSubstituicoes { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<GrupoAlimento> GruposAlimentos { get; set; }
        public DbSet<GrupoMuscular> GruposMusculares { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<PlanoTreino> PlanosTreinos { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Refeicao> Refeicoes { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<TreinoRelGrupoMuscular> TreinosRelGruposMusculares { get; set; }
        public DbSet<UnidadeMedida> UnidadesMedidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.Relational().ColumnType = "varchar(100)";

            modelBuilder.Entity<Paciente>().Ignore(p => p.CPF);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BioDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
