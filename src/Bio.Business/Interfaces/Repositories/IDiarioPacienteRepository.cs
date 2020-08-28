using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IDiarioPacienteRepository : IRepository<DiarioPaciente>
    {
        Task<DiarioPaciente> ObterDiarioPaciente(Guid id);

        Task<IEnumerable<DiarioPaciente>> ObterDiariosPacientes();

        Task<IEnumerable<DiarioPaciente>> ObterDiariosPacientesPorPaciente(Guid pacienteId);
    }
}
