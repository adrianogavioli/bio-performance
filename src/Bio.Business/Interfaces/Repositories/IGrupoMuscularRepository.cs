using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IGrupoMuscularRepository : IRepository<GrupoMuscular>
    {
        Task<GrupoMuscular> ObterGrupoMuscular(Guid id);
    }
}
