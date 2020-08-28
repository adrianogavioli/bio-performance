using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IUnidadeMedidaRepository : IRepository<UnidadeMedida>
    {
        Task<UnidadeMedida> ObterUnidadeMedida(Guid id);
    }
}
