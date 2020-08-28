using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class TreinoRelGrupoMuscularRepository : Repository<TreinoRelGrupoMuscular>, ITreinoRelGrupoMuscularRepository
    {
        public TreinoRelGrupoMuscularRepository(BioDbContext context) : base(context) {}

        public async Task<TreinoRelGrupoMuscular> ObterTreinoRelGrupoMuscular(Guid id)
        {
            return await _db.TreinosRelGruposMusculares
                .AsNoTracking()
                .Include(r => r.Treino)
                .Include(r => r.GrupoMuscular)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
