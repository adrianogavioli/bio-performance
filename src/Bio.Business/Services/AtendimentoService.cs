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
    public class AtendimentoService : BaseService, IAtendimentoService
    {
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IPlanoTreinoRepository _planoTreinoRepository;
        private readonly IDietaRepository _dietaRepository;

        public AtendimentoService(
            IAtendimentoRepository atendimentoRepository,
            IPlanoTreinoRepository planoTreinoRepository,
            IDietaRepository dietaRepository,
            INotificador notificador) : base(notificador)
        {
            _atendimentoRepository = atendimentoRepository;
            _planoTreinoRepository = planoTreinoRepository;
            _dietaRepository = dietaRepository;
        }

        public async Task Adicionar(Atendimento atendimento)
        {
            if (!ExecutarValidacao(new AtendimentoValidation(), atendimento)) return;

            await _atendimentoRepository.Adicionar(atendimento);
        }

        public async Task Atualizar(Atendimento atendimento)
        {
            atendimento.Profissional = null;
            atendimento.Paciente = null;

            if (!ExecutarValidacao(new AtendimentoValidation(), atendimento)) return;

            await _atendimentoRepository.Atualizar(atendimento);
        }

        public async Task Remover(Guid id)
        {
            if (_planoTreinoRepository.Buscar(p => p.AtendimentoId == id).Result.Any())
            {
                Notificar("Este atendimento possui planos de treinos, portanto não pode ser removido.");
                return;
            }

            if (_dietaRepository.Buscar(d => d.AtendimentoId == id).Result.Any())
            {
                Notificar("Este atendimento possui dietas, portanto não pode ser removido.");
                return;
            }

            await _atendimentoRepository.Remover(id);
        }

        public void Dispose()
        {
            _atendimentoRepository?.Dispose();
            _planoTreinoRepository?.Dispose();
            _dietaRepository?.Dispose();
        }
    }
}
