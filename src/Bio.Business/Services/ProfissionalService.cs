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
    public class ProfissionalService : BaseService, IProfissionalService
    {
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IAtendimentoRepository _atendimentoRepository;

        public ProfissionalService(IProfissionalRepository profissionalRepository,
            IAtendimentoRepository atendimentoRepository,
            INotificador notificador) : base(notificador)
        {
            _profissionalRepository = profissionalRepository;
            _atendimentoRepository = atendimentoRepository;
        }

        public async Task Adicionar(Profissional profissional)
        {
            if (!ExecutarValidacao(new ProfissionalValidation(), profissional)) return;

            if (_profissionalRepository.Buscar(p => p.Documento == profissional.Documento).Result.Any())
            {
                Notificar("O número do documento informado já existe, favor verificar.");
                return;
            }

            await _profissionalRepository.Adicionar(profissional);
        }

        public async Task Atualizar(Profissional profissional)
        {
            if (!ExecutarValidacao(new ProfissionalValidation(), profissional)) return;

            if (_profissionalRepository.Buscar(p => p.Documento == profissional.Documento && p.Id != profissional.Id).Result.Any())
            {
                Notificar("O número do documento informado já existe, favor verificar.");
                return;
            }

            await _profissionalRepository.Atualizar(profissional);
        }

        public async Task Remover(Guid id)
        {
            if (_atendimentoRepository.Buscar(a => a.ProfissionalId == id).Result.Any())
            {
                Notificar("Este profissional está sendo utilizado, portanto não pode ser removido. Considere inativa-lo.");
                return;
            }

            await _profissionalRepository.Remover(id);
        }

        public void Dispose()
        {
            _profissionalRepository?.Dispose();
            _atendimentoRepository?.Dispose();
        }
    }
}
