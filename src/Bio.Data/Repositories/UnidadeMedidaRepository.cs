using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class UnidadeMedidaRepository : Repository<UnidadeMedida>, IUnidadeMedidaRepository
    {
        public UnidadeMedidaRepository(BioDbContext context) : base(context) { }

        public async Task<UnidadeMedida> ObterUnidadeMedida(Guid id)
        {
            return await _db.UnidadesMedidas
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
