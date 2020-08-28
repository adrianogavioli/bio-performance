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
    public class AlimentoRepository : Repository<Alimento>, IAlimentoRepository
    {
        public AlimentoRepository(BioDbContext context) : base(context) { }

        public async Task<Alimento> ObterAlimento(Guid id)
        {
            return await _db.Alimentos
                .AsNoTracking()
                .Include(a => a.UnidadeMedida)
                .Include(a => a.GrupoAlimento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Alimento> ObterAlimentoSubstituicoes(Guid id)
        {
            return await _db.Alimentos
                .AsNoTracking()
                .Include(a => a.UnidadeMedida)
                .Include(a => a.GrupoAlimento)
                .Include(a => a.AlimentosSubstituicoes)
                    .ThenInclude(s => s.AlimentoSubstituto)
                        .ThenInclude(u => u.UnidadeMedida)
                .Include(a => a.AlimentosSubstituicoes)
                    .ThenInclude(s => s.AlimentoSubstituto)
                        .ThenInclude(g => g.GrupoAlimento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Alimento>> ObterAlimentos()
        {
            return await _db.Alimentos
                .AsNoTracking()
                .Include(a => a.UnidadeMedida)
                .Include(a => a.GrupoAlimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alimento>> ObterAlimentosPorGrupoAlimento(Guid grupoAlimentoId)
        {
            return await _db.Alimentos
                .AsNoTracking()
                .Include(a => a.UnidadeMedida)
                .Include(a => a.GrupoAlimento)
                .Where(a => a.GrupoAlimentoId == grupoAlimentoId)
                .ToListAsync();
        }

        public async Task<Alimento> ObterAlimentoPorDescricao(string descricao)
        {
            return await _db.Alimentos
                .AsNoTracking()
                .Include(a => a.UnidadeMedida)
                .Include(a => a.GrupoAlimento)
                .FirstOrDefaultAsync(a => a.Descricao == descricao);
        }
    }
}
