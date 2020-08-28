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
    public class RefeicaoService : BaseService, IRefeicaoService
    {
        private readonly IRefeicaoRepository _refeicaoRepository;
        private readonly IDietaRefeicaoAlimentoRepository _dietaRefeicaoAlimentoRepository;

        public RefeicaoService(IRefeicaoRepository refeicaoRepository,
            IDietaRefeicaoAlimentoRepository dietaRefeicaoAlimentoRepository,
            INotificador notificador) : base(notificador)
        {
            _refeicaoRepository = refeicaoRepository;
            _dietaRefeicaoAlimentoRepository = dietaRefeicaoAlimentoRepository;
        }

        public async Task Adicionar(Refeicao refeicao)
        {
            if (!ExecutarValidacao(new RefeicaoValidation(), refeicao)) return;

            if (_refeicaoRepository.Buscar(r => r.Ordem == refeicao.Ordem).Result.Any())
            {
                Notificar("A ordem informada já existe, favor verificar.");
                return;
            }

            if (_refeicaoRepository.Buscar(r => r.Descricao == refeicao.Descricao).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _refeicaoRepository.Adicionar(refeicao);
        }

        public async Task Atualizar(Refeicao refeicao)
        {
            if (!ExecutarValidacao(new RefeicaoValidation(), refeicao)) return;

            if (_refeicaoRepository.Buscar(r => r.Ordem == refeicao.Ordem && r.Id != refeicao.Id).Result.Any())
            {
                Notificar("A ordem informada já existe, favor verificar.");
                return;
            }

            if (_refeicaoRepository.Buscar(r => r.Descricao == refeicao.Descricao && r.Id != refeicao.Id).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _refeicaoRepository.Atualizar(refeicao);
        }

        public async Task Remover(Guid id)
        {
            if (_dietaRefeicaoAlimentoRepository.Buscar(r => r.RefeicaoId == id).Result.Any())
            {
                Notificar("Esta refeição está sendo utilizada, portanto não pode ser removida. Considere inativa-la.");
                return;
            }

            await _refeicaoRepository.Remover(id);
        }

        public void Dispose()
        {
            _refeicaoRepository?.Dispose();
            _dietaRefeicaoAlimentoRepository?.Dispose();
        }
    }
}
