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
    public class DietaRepository : Repository<Dieta>, IDietaRepository
    {
        public DietaRepository(BioDbContext context) : base(context) { }

        public async Task<Dieta> ObterDieta(Guid id)
        {
            return await _db.Dietas
                .AsNoTracking()
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Profissional)
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Dieta> ObterDietaRefeicoesAlimentos(Guid id)
        {
            return await _db.Dietas
                .AsNoTracking()
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Profissional)
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.Refeicao)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.Alimento)
                        .ThenInclude(a => a.UnidadeMedida)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.Alimento)
                        .ThenInclude(a => a.GrupoAlimento)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Dieta> ObterDietaRefeicoesSubstituicoes(Guid id)
        {
            return await _db.Dietas
                .AsNoTracking()
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Profissional)
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.Refeicao)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.Alimento)
                        .ThenInclude(a => a.UnidadeMedida)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.Alimento)
                        .ThenInclude(a => a.GrupoAlimento)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.DietasRefeicoesSubstituicoes)
                        .ThenInclude(s => s.Alimento)
                            .ThenInclude(a => a.UnidadeMedida)
                .Include(d => d.Refeicoes)
                    .ThenInclude(r => r.DietasRefeicoesSubstituicoes)
                        .ThenInclude(s => s.Alimento)
                            .ThenInclude(a => a.GrupoAlimento)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Dieta>> ObterDietas()
        {
            return await _db.Dietas
                .AsNoTracking()
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Profissional)
                .Include(d => d.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dieta>> ObterDietasPorAtendimento(Guid atendimentoId)
        {
            return await _db.Dietas
                .AsNoTracking()
                .Where(a => a.AtendimentoId == atendimentoId)
                .ToListAsync();
        }
    }
}
