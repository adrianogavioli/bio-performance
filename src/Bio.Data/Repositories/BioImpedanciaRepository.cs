using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class BioImpedanciaRepository : Repository<BioImpedancia>, IBioImpedanciaRepository
    {
        public BioImpedanciaRepository(BioDbContext context) : base(context) { }

        public async Task<BioImpedancia> ObterBioImpedancia(Guid id)
        {
            return await _db.BioImpedancias
                .AsNoTracking()
                .Include(b => b.Paciente)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BioImpedancia>> ObterBioImpedancias()
        {
            return await _db.BioImpedancias
                .AsNoTracking()
                .Include(b => b.Paciente)
                .ToListAsync();
        }
    }
}
