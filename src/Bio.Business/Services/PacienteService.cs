using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Bio.Business.Utilities;
using Bio.Business.Validations;
using Bio.Business.Validations.Documentos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.Business.Services
{
    public class PacienteService : BaseService, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IBioImpedanciaRepository _bioImpedanciaRepository;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IDiarioPacienteRepository _diarioPacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository,
            IBioImpedanciaRepository bioImpedanciaRepository,
            IAtendimentoRepository atendimentoRepository,
            IDiarioPacienteRepository diarioPacienteRepository,
            INotificador notificador) : base(notificador)
        {
            _pacienteRepository = pacienteRepository;
            _bioImpedanciaRepository = bioImpedanciaRepository;
            _atendimentoRepository = atendimentoRepository;
            _diarioPacienteRepository = diarioPacienteRepository;
        }

        public async Task Adicionar(Paciente paciente)
        {
            if (!CPFValidation.Validar(paciente.CPF))
            {
                Notificar("O CPF informado é inválido, favor verificar.");
                return;
            }

            paciente.CPF = StringFormat.ApenasNumeros(paciente.CPF);

            if (!ExecutarValidacao(new PacienteValidation(), paciente)) return;

            if (_pacienteRepository.Buscar(p => p.CPF == paciente.CPF).Result.Any())
            {
                Notificar("O CPF informado já existe, favor verificar.");
                return;
            }

            await _pacienteRepository.Adicionar(paciente);
        }

        public async Task Atualizar(Paciente paciente)
        {
            if (!CPFValidation.Validar(paciente.CPF))
            {
                Notificar("O CPF informado é inválido, favor verificar.");
                return;
            }

            paciente.CPF = StringFormat.ApenasNumeros(paciente.CPF);

            if (!ExecutarValidacao(new PacienteValidation(), paciente)) return;

            if (_pacienteRepository.Buscar(p => p.CPF == paciente.CPF && p.Id != paciente.Id).Result.Any())
            {
                Notificar("O CPF informado já existe, favor verificar.");
                return;
            }

            await _pacienteRepository.Atualizar(paciente);
        }

        public async Task Remover(Guid id)
        {
            if (_bioImpedanciaRepository.Buscar(b => b.PacienteId == id).Result.Any()
                || _atendimentoRepository.Buscar(a => a.PacienteId == id).Result.Any()
                || _diarioPacienteRepository.Buscar(d => d.PacienteId == id).Result.Any())
            {
                Notificar("Este paciente está sendo utilizado, portanto não pode ser removido.");
                return;
            }

            await _pacienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _pacienteRepository?.Dispose();
            _bioImpedanciaRepository?.Dispose();
            _atendimentoRepository?.Dispose();
            _diarioPacienteRepository?.Dispose();
        }
    }
}
