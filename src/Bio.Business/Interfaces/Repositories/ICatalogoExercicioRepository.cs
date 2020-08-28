using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface ICatalogoExercicioRepository : IRepository<CatalogoExercicio>
    {
        Task<CatalogoExercicio> ObterCatalogoExercicio(Guid id);

        Task<IEnumerable<CatalogoExercicio>> ObterCatalogoExercicios();
    }
}
