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
    public class DietaRefeicaoSubstituicaoService : BaseService, IDietaRefeicaoSubstituicaoService
    {
        private readonly IDietaRefeicaoSubstituicaoRepository _dietaRefeicaoSubstituicaoRepository;

        public DietaRefeicaoSubstituicaoService(IDietaRefeicaoSubstituicaoRepository dietaRefeicaoSubstituicaoRepository,
            INotificador notificador) : base(notificador)
        {
            _dietaRefeicaoSubstituicaoRepository = dietaRefeicaoSubstituicaoRepository;
        }

        public async Task Adicionar(DietaRefeicaoSubstituicao dietaRefeicaoSubstituicao)
        {
            dietaRefeicaoSubstituicao.Alimento = null;
            dietaRefeicaoSubstituicao.DietaRefeicaoAlimento = null;

            if (!ExecutarValidacao(new DietaRefeicaoSubstituicaoValidation(), dietaRefeicaoSubstituicao)) return;

            if (_dietaRefeicaoSubstituicaoRepository.Buscar(s => s.DietaRefeicaoAlimentoId == dietaRefeicaoSubstituicao.DietaRefeicaoAlimentoId
                                                            && s.AlimentoId == dietaRefeicaoSubstituicao.AlimentoId).Result.Any())
            {
                Notificar("Esta refeição já possui este alimento substituto.");
                return;
            }

            await _dietaRefeicaoSubstituicaoRepository.Adicionar(dietaRefeicaoSubstituicao);
        }

        public async Task Atualizar(DietaRefeicaoSubstituicao dietaRefeicaoSubstituicao)
        {
            if (!ExecutarValidacao(new DietaRefeicaoSubstituicaoValidation(), dietaRefeicaoSubstituicao)) return;

            await _dietaRefeicaoSubstituicaoRepository.Atualizar(dietaRefeicaoSubstituicao);
        }

        public async Task Remover(Guid id)
        {
            await _dietaRefeicaoSubstituicaoRepository.Remover(id);
        }

        public async Task RemoverPorRefeicao(Guid dietaRefeicaoAlimentoId)
        {
            await _dietaRefeicaoSubstituicaoRepository.RemoverPorRefeicao(dietaRefeicaoAlimentoId);
        }

        public void Dispose()
        {
            _dietaRefeicaoSubstituicaoRepository?.Dispose();
        }
    }
}
