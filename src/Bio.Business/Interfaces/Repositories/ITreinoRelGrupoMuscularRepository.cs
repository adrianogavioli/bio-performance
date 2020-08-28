using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface ITreinoRelGrupoMuscularRepository : IRepository<TreinoRelGrupoMuscular>
    {
        Task<TreinoRelGrupoMuscular> ObterTreinoRelGrupoMuscular(Guid id);
    }
}
