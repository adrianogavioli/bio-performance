using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IRefeicaoRepository : IRepository<Refeicao>
    {
        Task<Refeicao> ObterRefeicao(Guid id);
    }
}
