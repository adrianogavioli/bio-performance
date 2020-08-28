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
    public class AlimentoSubstituicaoService : BaseService, IAlimentoSubstituicaoService
    {
        private readonly IAlimentoSubstituicaoRepository _alimentoSubstituicaoRepository;
        private readonly IAlimentoRepository _alimentoRepository;

        public AlimentoSubstituicaoService(IAlimentoSubstituicaoRepository alimentoSubstituicaoRepository,
            IAlimentoRepository alimentoRepository,
            INotificador notificador) : base(notificador)
        {
            _alimentoSubstituicaoRepository = alimentoSubstituicaoRepository;
            _alimentoRepository = alimentoRepository;
        }

        public async Task Adicionar(AlimentoSubstituicao alimentoSubstituicao)
        {
            alimentoSubstituicao.Alimento = null;
            alimentoSubstituicao.AlimentoSubstituto = null;

            if (!ExecutarValidacao(new AlimentoSubstituicaoValidation(), alimentoSubstituicao)) return;

            if (alimentoSubstituicao.AlimentoId == alimentoSubstituicao.AlimentoSubstitutoId)
            {
                Notificar("O alimento substituto não deve ser igual ao alimento principal.");
                return;
            }

            if (_alimentoSubstituicaoRepository.Buscar(s => s.AlimentoId == alimentoSubstituicao.AlimentoId
                                                        && s.AlimentoSubstitutoId == alimentoSubstituicao.AlimentoSubstitutoId).Result.Any())
            {
                Notificar("O alimento principal selecionado, já possui esta substituição.");
                return;
            }

            var alimentoPrincipal = _alimentoRepository.Buscar(p => p.Id == alimentoSubstituicao.AlimentoId).Result.FirstOrDefault();
            var alimentoSubstituto = _alimentoRepository.Buscar(p => p.Id == alimentoSubstituicao.AlimentoSubstitutoId).Result.FirstOrDefault();

            if (alimentoSubstituto.UnidadeMedidaId != alimentoPrincipal.UnidadeMedidaId)
            {
                Notificar("A unidade de medida dos alimentos principal e substituto devem ser iguais.");
                return;
            }

            await _alimentoSubstituicaoRepository.Adicionar(alimentoSubstituicao);
        }

        public async Task Remover(Guid id)
        {
            await _alimentoSubstituicaoRepository.Remover(id);
        }

        public async Task RemoverPorAlimento(Guid alimentoId)
        {
            await _alimentoSubstituicaoRepository.RemoverPorAlimento(alimentoId);
        }

        public void Dispose()
        {
            _alimentoSubstituicaoRepository?.Dispose();
            _alimentoRepository?.Dispose();
        }
    }
}
