using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class GrupoAlimentoRepository : Repository<GrupoAlimento>, IGrupoAlimentoRepository
    {
        public GrupoAlimentoRepository(BioDbContext context) : base(context) { }

        public async Task<GrupoAlimento> ObterGrupoAlimento(Guid id)
        {
            return await _db.GruposAlimentos
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
