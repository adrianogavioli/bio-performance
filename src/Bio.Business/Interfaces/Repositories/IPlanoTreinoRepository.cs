using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IPlanoTreinoRepository : IRepository<PlanoTreino>
    {
        Task<PlanoTreino> ObterPlanoTreino(Guid id);

        Task<PlanoTreino> ObterPlanoTreinoTreinos(Guid id);

        Task<IEnumerable<PlanoTreino>> ObterPlanosTreinos();
    }
}
