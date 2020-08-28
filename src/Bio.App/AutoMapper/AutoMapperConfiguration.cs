using AutoMapper;
using Bio.App.ViewModels;
using Bio.Business.Models;

namespace Bio.App.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AlimentoSubstituicao, AlimentoSubstituicaoViewModel>().ReverseMap();
            CreateMap<Alimento, AlimentoViewModel>().ReverseMap();
            CreateMap<Atendimento, AtendimentoViewModel>().ReverseMap();
            CreateMap<BioImpedancia, BioImpedanciaViewModel>().ReverseMap();
            CreateMap<CatalogoExercicio, CatalogoExercicioViewModel>().ReverseMap();
            CreateMap<DiarioPaciente, DiarioPacienteViewModel>().ReverseMap();
            CreateMap<DietaRefeicaoAlimento, DietaRefeicaoAlimentoViewModel>().ReverseMap();
            CreateMap<DietaRefeicaoSubstituicao, DietaRefeicaoSubstituicaoViewModel>().ReverseMap();
            CreateMap<Dieta, DietaViewModel>().ReverseMap();
            CreateMap<Exercicio, ExercicioViewModel>().ReverseMap();
            CreateMap<GrupoAlimento, GrupoAlimentoViewModel>().ReverseMap();
            CreateMap<GrupoMuscular, GrupoMuscularViewModel>().ReverseMap();
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();
            CreateMap<PlanoTreino, PlanoTreinoViewModel>().ReverseMap();
            CreateMap<Profissional, ProfissionalViewModel>().ReverseMap();
            CreateMap<Refeicao, RefeicaoViewModel>().ReverseMap();
            CreateMap<TreinoRelGrupoMuscular, TreinoRelGrupoMuscularViewModel>().ReverseMap();
            CreateMap<Treino, TreinoViewModel>().ReverseMap();
            CreateMap<UnidadeMedida, UnidadeMedidaViewModel>().ReverseMap();
        }
    }
}
