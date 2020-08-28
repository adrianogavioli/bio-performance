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
    public class TreinoService : BaseService, ITreinoService
    {
        private readonly ITreinoRepository _treinoRepository;
        private readonly IDiarioPacienteRepository _diarioPacienteRepository;
        private readonly IExercicioService _exercicioService;
        private readonly ITreinoRelGrupoMuscularService _treinoRelGrupoMuscularService;

        public TreinoService(
            ITreinoRepository treinoRepository,
            IDiarioPacienteRepository diarioPacienteRepository,
            IExercicioService exercicioService,
            ITreinoRelGrupoMuscularService treinoRelGrupoMuscularService,
            INotificador notificador) : base(notificador)
        {
            _treinoRepository = treinoRepository;
            _diarioPacienteRepository = diarioPacienteRepository;
            _exercicioService = exercicioService;
            _treinoRelGrupoMuscularService = treinoRelGrupoMuscularService;
        }

        public async Task Adicionar(Treino treino)
        {
            if (!ExecutarValidacao(new TreinoValidation(), treino)) return;

            if (_treinoRepository.Buscar(t => t.PlanoTreinoId == treino.PlanoTreinoId && t.Ordem == treino.Ordem).Result.Any())
            {
                Notificar("A ordem do treino já existe no plano de treino.");
                return;
            }

            if (_treinoRepository.Buscar(t => t.PlanoTreinoId == treino.PlanoTreinoId && t.Titulo == treino.Titulo).Result.Any())
            {
                Notificar("O título do treino já existe no plano de treino.");
                return;
            }

            await _treinoRepository.Adicionar(treino);
        }

        public async Task Atualizar(Treino treino)
        {
            if (!ExecutarValidacao(new TreinoValidation(), treino)) return;

            if (_treinoRepository.Buscar(t => t.PlanoTreinoId == treino.PlanoTreinoId && t.Ordem == treino.Ordem && t.Id != treino.Id).Result.Any())
            {
                Notificar("A ordem do treino já existe no plano de treino.");
                return;
            }

            if (_treinoRepository.Buscar(t => t.PlanoTreinoId == treino.PlanoTreinoId && t.Titulo == treino.Titulo && t.Id != treino.Id).Result.Any())
            {
                Notificar("O título do treino já existe no plano de treino.");
                return;
            }

            await _treinoRepository.Atualizar(treino);
        }

        public async Task Remover(Guid id)
        {
            if (_diarioPacienteRepository.Buscar(d => d.TreinoId == id).Result.Any())
            {
                Notificar("Este treino está sendo utilizado, portanto não pode ser removido. Considere inativa-lo.");
                return;
            }

            await _exercicioService.RemoverPorTreino(id);

            await _treinoRelGrupoMuscularService.RemoverPorTreino(id);

            await _treinoRepository.Remover(id);
        }

        public void Dispose()
        {
            _treinoRepository?.Dispose();
            _diarioPacienteRepository?.Dispose();
            _exercicioService?.Dispose();
            _treinoRelGrupoMuscularService?.Dispose();
        }
    }
}
