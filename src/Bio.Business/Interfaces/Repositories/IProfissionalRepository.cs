using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IProfissionalRepository : IRepository<Profissional>
    {
        Task<Profissional> ObterProfissional(Guid id);

        Task<Profissional> ObterProfissionalAtendimentos(Guid id);
    }
}
