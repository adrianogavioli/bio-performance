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
    public class ExercicioRepository : Repository<Exercicio>, IExercicioRepository
    {
        public ExercicioRepository(BioDbContext context) : base(context) {}

        public async Task<Exercicio> ObterExercicio(Guid id)
        {
            return await _db.Exercicios
                .AsNoTracking()
                .Include(e => e.Treino)
                    .ThenInclude(t => t.GruposMusculares)
                        .ThenInclude(g => g.GrupoMuscular)
                .Include(e => e.CatalogoExercicio)
                    .ThenInclude(c => c.GrupoMuscular)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Exercicio>> ObterExerciciosPorTreino(Guid treinoId)
        {
            return await _db.Exercicios
                .AsNoTracking()
                .Include(e => e.CatalogoExercicio)
                    .ThenInclude(c => c.GrupoMuscular)
                .Where(e => e.TreinoId == treinoId)
                .ToListAsync();
        }
    }
}
