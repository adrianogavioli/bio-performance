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
    public class DietaRefeicaoAlimentoService : BaseService, IDietaRefeicaoAlimentoService
    {
        private readonly IDietaRefeicaoAlimentoRepository _dietaRefeicaoAlimentoRepository;
        private readonly IDietaRefeicaoSubstituicaoRepository _dietaRefeicaoSubstituicaoRepository;

        public DietaRefeicaoAlimentoService(IDietaRefeicaoAlimentoRepository dietaRefeicaoAlimentoRepository,
            IDietaRefeicaoSubstituicaoRepository dietaRefeicaoSubstituicaoRepository,
            INotificador notificador) : base(notificador)
        {
            _dietaRefeicaoAlimentoRepository = dietaRefeicaoAlimentoRepository;
            _dietaRefeicaoSubstituicaoRepository = dietaRefeicaoSubstituicaoRepository;
        }

        public async Task Adicionar(DietaRefeicaoAlimento dietaRefeicaoAlimento)
        {
            dietaRefeicaoAlimento.Dieta = null;
            dietaRefeicaoAlimento.Refeicao = null;
            dietaRefeicaoAlimento.Alimento = null;

            if (!ExecutarValidacao(new DietaRefeicaoAlimentoValidation(), dietaRefeicaoAlimento)) return;

            if (_dietaRefeicaoAlimentoRepository.Buscar(r => r.DietaId == dietaRefeicaoAlimento.DietaId
                                                        && r.RefeicaoId == dietaRefeicaoAlimento.RefeicaoId
                                                        && r.AlimentoId == dietaRefeicaoAlimento.AlimentoId).Result.Any())
            {
                Notificar("Este alimento já foi adicionado a esta refeição.");
                return;
            }

            await _dietaRefeicaoAlimentoRepository.Adicionar(dietaRefeicaoAlimento);
        }

        public async Task Atualizar(DietaRefeicaoAlimento dietaRefeicaoAlimento)
        {
            if (!ExecutarValidacao(new DietaRefeicaoAlimentoValidation(), dietaRefeicaoAlimento)) return;

            await _dietaRefeicaoAlimentoRepository.Atualizar(dietaRefeicaoAlimento);
        }

        public async Task Remover(Guid id)
        {
            await _dietaRefeicaoSubstituicaoRepository.RemoverPorRefeicao(id);

            await _dietaRefeicaoAlimentoRepository.Remover(id);
        }

        public async Task RemoverPorDieta(Guid dietaId)
        {
            await _dietaRefeicaoAlimentoRepository.RemoverPorDieta(dietaId);
        }

        public void Dispose()
        {
            _dietaRefeicaoAlimentoRepository?.Dispose();
            _dietaRefeicaoSubstituicaoRepository?.Dispose();
        }
    }
}
