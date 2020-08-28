using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Notifications;
using Bio.Business.Services;
using Bio.Data.Context;
using Bio.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bio.App.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BioDbContext>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IAlimentoRepository, AlimentoRepository>();
            services.AddScoped<IAlimentoSubstituicaoRepository, AlimentoSubstituicaoRepository>();
            services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
            services.AddScoped<IBioImpedanciaRepository, BioImpedanciaRepository>();
            services.AddScoped<ICatalogoExercicioRepository, CatalogoExercicioRepository>();
            services.AddScoped<IDiarioPacienteRepository, DiarioPacienteRepository>();
            services.AddScoped<IDietaRefeicaoAlimentoRepository, DietaRefeicaoAlimentoRepository>();
            services.AddScoped<IDietaRefeicaoSubstituicaoRepository, DietaRefeicaoSubstituicaoRepository>();
            services.AddScoped<IDietaRepository, DietaRepository>();
            services.AddScoped<IExercicioRepository, ExercicioRepository>();
            services.AddScoped<IGrupoAlimentoRepository, GrupoAlimentoRepository>();
            services.AddScoped<IGrupoMuscularRepository, GrupoMuscularRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPlanoTreinoRepository, PlanoTreinoRepository>();
            services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
            services.AddScoped<IRefeicaoRepository, RefeicaoRepository>();
            services.AddScoped<ITreinoRelGrupoMuscularRepository, TreinoRelGrupoMuscularRepository>();
            services.AddScoped<ITreinoRepository, TreinoRepository>();
            services.AddScoped<IUnidadeMedidaRepository, UnidadeMedidaRepository>();

            services.AddScoped<IAlimentoService, AlimentoService>();
            services.AddScoped<IAlimentoSubstituicaoService, AlimentoSubstituicaoService>();
            services.AddScoped<IAtendimentoService, AtendimentoService>();
            services.AddScoped<IBioImpedanciaService, BioImpedanciaService>();
            services.AddScoped<ICatalogoExercicioService, CatalogoExercicioService>();
            services.AddScoped<IDiarioPacienteService, DiarioPacienteService>();
            services.AddScoped<IDietaRefeicaoAlimentoService, DietaRefeicaoAlimentoService>();
            services.AddScoped<IDietaRefeicaoSubstituicaoService, DietaRefeicaoSubstituicaoService>();
            services.AddScoped<IDietaService, DietaService>();
            services.AddScoped<IExercicioService, ExercicioService>();
            services.AddScoped<IGrupoAlimentoService, GupoAlimentoService>();
            services.AddScoped<IGrupoMuscularService, GrupoMuscularService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IPlanoTreinoService, PlanoTreinoService>();
            services.AddScoped<IProfissionalService, ProfissionalService>();
            services.AddScoped<IRefeicaoService, RefeicaoService>();
            services.AddScoped<ITreinoRelGrupoMuscularService, TreinoRelGrupoMuscularService>();
            services.AddScoped<ITreinoService, TreinoService>();
            services.AddScoped<IUnidadeMedidaService, UnidadeMedidaService>();

            return services;
        }
    }
}
