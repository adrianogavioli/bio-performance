using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class CatalogoExercicioRepository : Repository<CatalogoExercicio>, ICatalogoExercicioRepository
    {
        public CatalogoExercicioRepository(BioDbContext context) : base(context) {}

        public async Task<CatalogoExercicio> ObterCatalogoExercicio(Guid id)
        {
            return await _db.CatalogoExercicios
                .AsNoTracking()
                .Include(c => c.GrupoMuscular)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CatalogoExercicio>> ObterCatalogoExercicios()
        {
            return await _db.CatalogoExercicios
                .AsNoTracking()
                .Include(c => c.GrupoMuscular)
                .ToListAsync();
        }
    }
}
