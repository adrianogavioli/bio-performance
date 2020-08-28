using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IExercicioRepository : IRepository<Exercicio>
    {
        Task<Exercicio> ObterExercicio(Guid id);

        Task<IEnumerable<Exercicio>> ObterExerciciosPorTreino(Guid treinoId);
    }
}
