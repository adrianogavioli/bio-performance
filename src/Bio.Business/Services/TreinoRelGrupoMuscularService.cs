using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Bio.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.Business.Services
{
    public class TreinoRelGrupoMuscularService : BaseService, ITreinoRelGrupoMuscularService
    {
        private readonly ITreinoRelGrupoMuscularRepository _treinoRelGrupoMuscularRepository;
        private readonly IExercicioRepository _exercicioRepository;

        public TreinoRelGrupoMuscularService(
            ITreinoRelGrupoMuscularRepository treinoRelGrupoMuscularRepository,
            IExercicioRepository exercicioRepository,
            INotificador notificador) : base(notificador)
        {
            _treinoRelGrupoMuscularRepository = treinoRelGrupoMuscularRepository;
            _exercicioRepository = exercicioRepository;
        }

        public async Task Adicionar(TreinoRelGrupoMuscular treinoRelGrupoMuscular)
        {
            if (!ExecutarValidacao(new TreinoRelGrupoMuscularValidation(), treinoRelGrupoMuscular)) return;

            if (_treinoRelGrupoMuscularRepository.Buscar(r => r.TreinoId == treinoRelGrupoMuscular.TreinoId
                                                        && r.GrupoMuscularId == treinoRelGrupoMuscular.GrupoMuscularId).Result.Any())
            {
                Notificar("Este treino já possui relação com o grupo muscular informado.");
                return;
            }

            await _treinoRelGrupoMuscularRepository.Adicionar(treinoRelGrupoMuscular);
        }

        public async Task Remover(Guid id)
        {
            var relacao = await _treinoRelGrupoMuscularRepository.ObterTreinoRelGrupoMuscular(id);

            if (relacao == null)
            {
                Notificar("A relação entre o treino e o grupo muscular informados não existe, verifique se já foi removida.");
                return;
            }

            var exercicios = await _exercicioRepository.ObterExerciciosPorTreino(relacao.TreinoId);

            if (exercicios.Any(e => e.CatalogoExercicio.GrupoMuscularId == relacao.GrupoMuscularId))
            {
                Notificar("Este treino possui exercícios relacionados a este grupo muscular, portanto não pode ser removido.");
                return;
            }

            await _treinoRelGrupoMuscularRepository.Remover(id);
        }

        public async Task RemoverPorTreino(Guid treinoId)
        {
            var relacoes = await _treinoRelGrupoMuscularRepository.Buscar(r => r.TreinoId == treinoId);

            foreach (var relacao in relacoes)
            {
                await _treinoRelGrupoMuscularRepository.Remover(relacao.Id);
            }
        }

        public void Dispose()
        {
            _treinoRelGrupoMuscularRepository?.Dispose();
            _exercicioRepository?.Dispose();
        }
    }
}
