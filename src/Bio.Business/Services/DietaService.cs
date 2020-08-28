using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Bio.Business.Validations;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Services
{
    public class DietaService : BaseService, IDietaService
    {
        private readonly IDietaRepository _dietaRepository;
        private readonly IDietaRefeicaoAlimentoRepository _dietaRefeicaoAlimentoRepository;
        private readonly IDietaRefeicaoSubstituicaoRepository _dietaRefeicaoSubstituicaoRepository;

        public DietaService(IDietaRepository dietaRepository,
            IDietaRefeicaoAlimentoRepository dietaRefeicaoAlimentoRepository,
            IDietaRefeicaoSubstituicaoRepository dietaRefeicaoSubstituicaoRepository,
            INotificador notificador) : base(notificador)
        {
            _dietaRepository = dietaRepository;
            _dietaRefeicaoAlimentoRepository = dietaRefeicaoAlimentoRepository;
            _dietaRefeicaoSubstituicaoRepository = dietaRefeicaoSubstituicaoRepository;
        }

        public async Task Adicionar(Dieta dieta)
        {
            dieta.Atendimento = null;

            if (!ExecutarValidacao(new DietaValidation(), dieta)) return;

            await _dietaRepository.Adicionar(dieta);
        }

        public async Task Atualizar(Dieta dieta)
        {
            if (!ExecutarValidacao(new DietaValidation(), dieta)) return;

            await _dietaRepository.Atualizar(dieta);
        }

        public async Task Remover(Guid id)
        {
            var dieta = await _dietaRepository.ObterDietaRefeicoesAlimentos(id);

            foreach (var refeicao in dieta.Refeicoes)
            {
                await _dietaRefeicaoSubstituicaoRepository.RemoverPorRefeicao(refeicao.Id);
            }

            await _dietaRefeicaoAlimentoRepository.RemoverPorDieta(id);

            await _dietaRepository.Remover(id);
        }

        public void Dispose()
        {
            _dietaRepository?.Dispose();
            _dietaRefeicaoAlimentoRepository?.Dispose();
            _dietaRefeicaoSubstituicaoRepository?.Dispose();
        }
    }
}
