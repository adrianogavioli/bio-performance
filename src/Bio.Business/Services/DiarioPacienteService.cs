using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Bio.Business.Validations;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Services
{
    public class DiarioPacienteService : BaseService, IDiarioPacienteService
    {
        private readonly IDiarioPacienteRepository _diarioPacienteRepository;

        public DiarioPacienteService(
            IDiarioPacienteRepository diarioPacienteRepository,
            INotificador notificador) : base(notificador)
        {
            _diarioPacienteRepository = diarioPacienteRepository;
        }

        public async Task Adicionar(DiarioPaciente diarioPaciente)
        {
            if (!ExecutarValidacao(new DiarioPacienteValidation(), diarioPaciente)) return;

            await _diarioPacienteRepository.Adicionar(diarioPaciente);
        }

        public async Task Remover(Guid id)
        {
            await _diarioPacienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _diarioPacienteRepository?.Dispose();
        }
    }
}
