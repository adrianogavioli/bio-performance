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
    public class UnidadeMedidaService : BaseService, IUnidadeMedidaService
    {
        private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;
        private readonly IAlimentoRepository _alimentoRepository;

        public UnidadeMedidaService(IUnidadeMedidaRepository unidadeMedidaRepository,
            IAlimentoRepository alimentoRepository,
            INotificador notificador) : base(notificador)
        {
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _alimentoRepository = alimentoRepository;
        }

        public async Task Adicionar(UnidadeMedida unidadeMedida)
        {
            if (!ExecutarValidacao(new UnidadeMedidaValidation(), unidadeMedida)) return;

            if (_unidadeMedidaRepository.Buscar(u => u.Codigo == unidadeMedida.Codigo).Result.Any())
            {
                Notificar("O código informado já existe, favor verificar.");
                return;
            }

            if (_unidadeMedidaRepository.Buscar(u => u.Descricao== unidadeMedida.Descricao).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _unidadeMedidaRepository.Adicionar(unidadeMedida);
        }

        public async Task Atualizar(UnidadeMedida unidadeMedida)
        {
            if (!ExecutarValidacao(new UnidadeMedidaValidation(), unidadeMedida)) return;

            if (_unidadeMedidaRepository.Buscar(u => u.Codigo == unidadeMedida.Codigo && u.Id != unidadeMedida.Id).Result.Any())
            {
                Notificar("O código informado já existe, favor verificar.");
                return;
            }

            if (_unidadeMedidaRepository.Buscar(u => u.Descricao == unidadeMedida.Descricao && u.Id != unidadeMedida.Id).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _unidadeMedidaRepository.Atualizar(unidadeMedida);
        }

        public async Task Remover(Guid id)
        {
            if (_alimentoRepository.Buscar(a => a.UnidadeMedidaId == id).Result.Any())
            {
                Notificar("Esta unidade de medida está sendo utilizada, portanto não pode ser removida. Considere inativa-la.");
                return;
            }

            await _unidadeMedidaRepository.Remover(id);
        }

        public void Dispose()
        {
            _unidadeMedidaRepository?.Dispose();
            _alimentoRepository?.Dispose();
        }
    }
}
