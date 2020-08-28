using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Bio.Business.Validations;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Services
{
    public class BioImpedanciaService : BaseService, IBioImpedanciaService
    {
        private readonly IBioImpedanciaRepository _bioImpedanciaRepository;

        public BioImpedanciaService(IBioImpedanciaRepository bioImpedanciaRepository,
            INotificador notificador) : base(notificador)
        {
            _bioImpedanciaRepository = bioImpedanciaRepository;
        }

        public async Task Adicionar(BioImpedancia bioImpedancia)
        {
            if (!ExecutarValidacao(new BioImpedanciaValidation(), bioImpedancia)) return;

            await _bioImpedanciaRepository.Adicionar(bioImpedancia);
        }

        public async Task Atualizar(BioImpedancia bioImpedancia)
        {
            if (!ExecutarValidacao(new BioImpedanciaValidation(), bioImpedancia)) return;

            await _bioImpedanciaRepository.Atualizar(bioImpedancia);
        }

        public async Task Remover(Guid id)
        {
            await _bioImpedanciaRepository.Remover(id);
        }

        public void Dispose()
        {
            _bioImpedanciaRepository?.Dispose();
        }
    }
}
