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
    public class GupoAlimentoService : BaseService, IGrupoAlimentoService
    {
        private readonly IGrupoAlimentoRepository _grupoAlimentoRepository;
        private readonly IAlimentoRepository _alimentoRepository;

        public GupoAlimentoService(IGrupoAlimentoRepository grupoAlimentoRepository,
            IAlimentoRepository alimentoRepository,
            INotificador notificador) : base(notificador)
        {
            _grupoAlimentoRepository = grupoAlimentoRepository;
            _alimentoRepository = alimentoRepository;
        }

        public async Task Adicionar(GrupoAlimento grupoAlimento)
        {
            if (!ExecutarValidacao(new GrupoAlimentoValidation(), grupoAlimento)) return;

            if (_grupoAlimentoRepository.Buscar(g => g.Codigo == grupoAlimento.Codigo).Result.Any())
            {
                Notificar("O código informado já existe, favor verificar.");
                return;
            }

            if (_grupoAlimentoRepository.Buscar(g => g.Descricao == grupoAlimento.Descricao).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _grupoAlimentoRepository.Adicionar(grupoAlimento);
        }

        public async Task Atualizar(GrupoAlimento grupoAlimento)
        {
            if (!ExecutarValidacao(new GrupoAlimentoValidation(), grupoAlimento)) return;

            if (_grupoAlimentoRepository.Buscar(g => g.Codigo == grupoAlimento.Codigo && g.Id != grupoAlimento.Id).Result.Any())
            {
                Notificar("O código informado já existe, favor verificar.");
                return;
            }

            if (_grupoAlimentoRepository.Buscar(g => g.Descricao == grupoAlimento.Descricao && g.Id != grupoAlimento.Id).Result.Any())
            {
                Notificar("A descrição informada já existe, favor verificar.");
                return;
            }

            await _grupoAlimentoRepository.Atualizar(grupoAlimento);
        }

        public async Task Remover(Guid id)
        {
            if (_alimentoRepository.Buscar(a => a.GrupoAlimentoId == id).Result.Any())
            {
                Notificar("Este grupo de alimento está sendo utilizado, portanto não pode ser removido. Considere inativa-lo.");
                return;
            }

            await _grupoAlimentoRepository.Remover(id);
        }

        public void Dispose()
        {
            _grupoAlimentoRepository?.Dispose();
            _alimentoRepository?.Dispose();
        }
    }
}
