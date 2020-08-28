using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class DietaRefeicaoAlimentoRepository : Repository<DietaRefeicaoAlimento>, IDietaRefeicaoAlimentoRepository
    {
        public DietaRefeicaoAlimentoRepository(BioDbContext context) : base(context) { }

        public async Task<DietaRefeicaoAlimento> ObterDietaRefeicaoAlimento(Guid id)
        {
            return await _db.DietasRefeicoesAlimentos
                .AsNoTracking()
                .Include(d => d.Refeicao)
                .Include(d => d.Alimento)
                    .ThenInclude(a => a.UnidadeMedida)
                .Include(d => d.Alimento)
                    .ThenInclude(a => a.GrupoAlimento)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DietaRefeicaoAlimento> ObterDietaRefeicaoAlimentoSubstituicoes(Guid id)
        {
            return await _db.DietasRefeicoesAlimentos
                .AsNoTracking()
                .Include(d => d.Refeicao)
                .Include(d => d.Alimento)
                    .ThenInclude(a => a.UnidadeMedida)
                .Include(d => d.Alimento)
                    .ThenInclude(a => a.GrupoAlimento)
                .Include(d => d.DietasRefeicoesSubstituicoes)
                    .ThenInclude(s => s.Alimento)
                        .ThenInclude(a => a.UnidadeMedida)
                .Include(d => d.DietasRefeicoesSubstituicoes)
                    .ThenInclude(s => s.Alimento)
                        .ThenInclude(a => a.GrupoAlimento)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task RemoverPorDieta(Guid dietaId)
        {
            _db.DietasRefeicoesAlimentos
                .RemoveRange(await Buscar(r => r.DietaId == dietaId));

            await SaveChanges();
        }
    }
}
