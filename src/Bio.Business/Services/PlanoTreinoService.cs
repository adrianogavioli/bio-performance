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
    public class PlanoTreinoService : BaseService, IPlanoTreinoService
    {
        private readonly IPlanoTreinoRepository _planoTreinoRepository;
        private readonly ITreinoRepository _treinoRepository;

        public PlanoTreinoService(
            IPlanoTreinoRepository planoTreinoRepository,
            ITreinoRepository treinoRepository,
            INotificador notificador) : base(notificador)
        {
            _planoTreinoRepository = planoTreinoRepository;
            _treinoRepository = treinoRepository;
        }

        public async Task Adicionar(PlanoTreino planoTreino)
        {
            planoTreino.Atendimento = null;

            if (!ExecutarValidacao(new PlanoTreinoValidation(), planoTreino)) return;

            await _planoTreinoRepository.Adicionar(planoTreino);
        }

        public async Task Atualizar(PlanoTreino planoTreino)
        {
            if (!ExecutarValidacao(new PlanoTreinoValidation(), planoTreino)) return;

            await _planoTreinoRepository.Atualizar(planoTreino);
        }

        public async Task Remover(Guid id)
        {
            if (_treinoRepository.Buscar(t => t.PlanoTreinoId == id).Result.Any())
            {
                Notificar("Este plano de treino possui treinos, portanto não pode ser removido. Considere inativa-lo.");
                return;
            }

            await _planoTreinoRepository.Remover(id);
        }

        public void Dispose()
        {
            _planoTreinoRepository?.Dispose();
            _treinoRepository?.Dispose();
        }
    }
}
