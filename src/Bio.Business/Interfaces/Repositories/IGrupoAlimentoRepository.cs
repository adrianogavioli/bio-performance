using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IGrupoAlimentoRepository : IRepository<GrupoAlimento>
    {
        Task<GrupoAlimento> ObterGrupoAlimento(Guid id);
    }
}
