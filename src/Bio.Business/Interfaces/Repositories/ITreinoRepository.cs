using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface ITreinoRepository : IRepository<Treino>
    {
        Task<Treino> ObterTreino(Guid id);

        Task<Treino> ObterTreinoExercicios(Guid id);

        Task<IEnumerable<Treino>> ObterTreinosAtivosPorPaciente(Guid pacienteId);
    }
}
