using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class DietaRefeicaoSubstituicaoRepository : Repository<DietaRefeicaoSubstituicao>, IDietaRefeicaoSubstituicaoRepository
    {
        public DietaRefeicaoSubstituicaoRepository(BioDbContext context) : base(context) { }

        public async Task<DietaRefeicaoSubstituicao> ObterDietaRefeicaoSubstituicao(Guid id)
        {
            return await _db.DietasRefeicoesSubstituicoes
                .AsNoTracking()
                .Include(s => s.DietaRefeicaoAlimento)
                    .ThenInclude(r => r.Refeicao)
                .Include(s => s.DietaRefeicaoAlimento)
                    .ThenInclude(r => r.Alimento)
                        .ThenInclude(a => a.UnidadeMedida)
                .Include(s => s.DietaRefeicaoAlimento)
                    .ThenInclude(r => r.Alimento)
                        .ThenInclude(a => a.GrupoAlimento)
                .Include(s => s.Alimento)
                    .ThenInclude(a => a.UnidadeMedida)
                .Include(s => s.Alimento)
                    .ThenInclude(a => a.GrupoAlimento)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoverPorRefeicao(Guid dietaRefeicaoAlimentoId)
        {
            _db.DietasRefeicoesSubstituicoes
                .RemoveRange(await Buscar(s => s.DietaRefeicaoAlimentoId == dietaRefeicaoAlimentoId));

            await SaveChanges();
        }
    }
}
