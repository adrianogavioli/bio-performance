using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class AlimentoSubstituicaoRepository : Repository<AlimentoSubstituicao>, IAlimentoSubstituicaoRepository
    {
        public AlimentoSubstituicaoRepository(BioDbContext context) : base(context) { }

        public async Task<AlimentoSubstituicao> ObterAlimentoSubstituicao(Guid id)
        {
            return await _db.AlimentosSubstituicoes
                .AsNoTracking()
                .Include(a => a.Alimento)
                    .ThenInclude(u => u.UnidadeMedida)
                .Include(a => a.Alimento)
                    .ThenInclude(g => g.GrupoAlimento)
                .Include(a => a.AlimentoSubstituto)
                    .ThenInclude(u => u.UnidadeMedida)
                .Include(a => a.AlimentoSubstituto)
                    .ThenInclude(g => g.GrupoAlimento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AlimentoSubstituicao>> ObterAlimentosSubstituicoesPorAlimentoId(Guid alimentoId)
        {
            return await _db.AlimentosSubstituicoes
                .AsNoTracking()
                .Include(a => a.AlimentoSubstituto)
                    .ThenInclude(u => u.UnidadeMedida)
                .Include(a => a.AlimentoSubstituto)
                    .ThenInclude(g => g.GrupoAlimento)
                .Where(a => a.AlimentoId == alimentoId)
                .ToListAsync();
        }

        public async Task RemoverPorAlimento(Guid alimentoId)
        {
            _db.AlimentosSubstituicoes
                .RemoveRange(await Buscar(s => s.AlimentoId == alimentoId));

            await SaveChanges();
        }
    }
}
