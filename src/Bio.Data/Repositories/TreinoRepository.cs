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
    public class TreinoRepository : Repository<Treino>, ITreinoRepository
    {
        public TreinoRepository(BioDbContext context) : base(context) {}

        public async Task<Treino> ObterTreino(Guid id)
        {
            return await _db.Treinos
                .AsNoTracking()
                .Include(t => t.GruposMusculares)
                    .ThenInclude(g => g.GrupoMuscular)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Treino> ObterTreinoExercicios(Guid id)
        {
            return await _db.Treinos
                .AsNoTracking()
                .Include(t => t.GruposMusculares)
                    .ThenInclude(g => g.GrupoMuscular)
                .Include(t => t.Exercicios)
                    .ThenInclude(e => e.CatalogoExercicio)
                        .ThenInclude(c => c.GrupoMuscular)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Treino>> ObterTreinosAtivosPorPaciente(Guid pacienteId)
        {
            return await _db.Treinos
                .AsNoTracking()
                .Where(t => t.Ativo
                    && t.PlanoTreino.Atendimento.PacienteId == pacienteId)
                .ToListAsync();
        }
    }
}
