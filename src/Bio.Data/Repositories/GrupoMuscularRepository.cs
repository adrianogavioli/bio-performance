using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class GrupoMuscularRepository : Repository<GrupoMuscular>, IGrupoMuscularRepository
    {
        public GrupoMuscularRepository(BioDbContext context) : base(context) {}

        public async Task<GrupoMuscular> ObterGrupoMuscular(Guid id)
        {
            return await _db.GruposMusculares
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
