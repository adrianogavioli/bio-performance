using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class RefeicaoRepository : Repository<Refeicao>, IRefeicaoRepository
    {
        public RefeicaoRepository(BioDbContext context) : base(context) { }

        public async Task<Refeicao> ObterRefeicao(Guid id)
        {
            return await _db.Refeicoes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
