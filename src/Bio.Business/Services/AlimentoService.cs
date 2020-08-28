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
    public class AlimentoService : BaseService, IAlimentoService
    {
        private readonly IAlimentoRepository _alimentoRepository;
        private readonly IDietaRefeicaoAlimentoRepository _dietaRefeicaoAlimentoRepository;
        private readonly IAlimentoSubstituicaoRepository _alimentoSubstituicaoRepository;

        public AlimentoService(IAlimentoRepository alimentoRepository,
            IDietaRefeicaoAlimentoRepository dietaRefeicaoAlimentoRepository,
            IAlimentoSubstituicaoRepository alimentoSubstituicaoRepository,
            INotificador notificador) : base(notificador)
        {
            _alimentoRepository = alimentoRepository;
            _dietaRefeicaoAlimentoRepository = dietaRefeicaoAlimentoRepository;
            _alimentoSubstituicaoRepository = alimentoSubstituicaoRepository;
        }

        public async Task Adicionar(Alimento alimento)
        {
            if (!ExecutarValidacao(new AlimentoValidation(), alimento)) return;

            if (_alimentoRepository.Buscar(a => a.Descricao == alimento.Descricao).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _alimentoRepository.Adicionar(alimento);
        }

        public async Task Atualizar(Alimento alimento)
        {
            if (!ExecutarValidacao(new AlimentoValidation(), alimento)) return;

            if (_alimentoRepository.Buscar(a => a.Descricao == alimento.Descricao && a.Id != alimento.Id).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _alimentoRepository.Atualizar(alimento);
        }

        public async Task Remover(Guid id)
        {
            if (_dietaRefeicaoAlimentoRepository.Buscar(r => r.AlimentoId == id).Result.Any()
                || _alimentoSubstituicaoRepository.Buscar(s => s.AlimentoSubstitutoId == id).Result.Any())
            {
                Notificar("Este alimento está sendo utilizado, portanto não pode ser removido.");
                return;
            }

            await _alimentoSubstituicaoRepository.RemoverPorAlimento(id);

            await _alimentoRepository.Remover(id);
        }

        public void Dispose()
        {
            _alimentoRepository?.Dispose();
            _dietaRefeicaoAlimentoRepository?.Dispose();
            _alimentoSubstituicaoRepository?.Dispose();
        }
    }
}
