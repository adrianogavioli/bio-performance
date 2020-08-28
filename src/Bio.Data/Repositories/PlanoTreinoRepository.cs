using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class PlanoTreinoRepository : Repository<PlanoTreino>, IPlanoTreinoRepository
    {
        public PlanoTreinoRepository(BioDbContext context) : base(context) {}

        public async Task<PlanoTreino> ObterPlanoTreino(Guid id)
        {
            return await _db.PlanosTreinos
                .AsNoTracking()
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Profissional)
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PlanoTreino> ObterPlanoTreinoTreinos(Guid id)
        {
            return await _db.PlanosTreinos
                .AsNoTracking()
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Profissional)
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .Include(p => p.Treinos)
                    .ThenInclude(t => t.GruposMusculares)
                        .ThenInclude(g => g.GrupoMuscular)
                .Include(p => p.Treinos)
                    .ThenInclude(t => t.Exercicios)
                        .ThenInclude(e => e.CatalogoExercicio)
                            .ThenInclude(c => c.GrupoMuscular)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PlanoTreino>> ObterPlanosTreinos()
        {
            return await _db.PlanosTreinos
                .AsNoTracking()
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Profissional)
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .ToListAsync();
        }
    }
}
