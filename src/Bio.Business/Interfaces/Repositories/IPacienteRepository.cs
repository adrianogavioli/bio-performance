using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<Paciente> ObterPaciente(Guid id);

        Task<Paciente> ObterPacienteBioImpedancias(Guid id);

        Task<Paciente> ObterPacienteAtendimentos(Guid id);

        Task<Paciente> ObterPacienteDiarios(Guid id);

        Task<Paciente> ObterPacienteBioImpedanciasAtendimentosDiarios(Guid id);
    }
}
