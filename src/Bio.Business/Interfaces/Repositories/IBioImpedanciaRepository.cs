using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IBioImpedanciaRepository : IRepository<BioImpedancia>
    {
        Task<BioImpedancia> ObterBioImpedancia(Guid id);

        Task<IEnumerable<BioImpedancia>> ObterBioImpedancias();
    }
}
