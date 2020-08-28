using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class ProfissionalRepository : Repository<Profissional>, IProfissionalRepository
    {
        public ProfissionalRepository(BioDbContext context) : base(context) { }

        public async Task<Profissional> ObterProfissional(Guid id)
        {
            return await _db.Profissionais
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Profissional> ObterProfissionalAtendimentos(Guid id)
        {
            return await _db.Profissionais
                .AsNoTracking()
                .Include(p => p.Atendimentos)
                    .ThenInclude(a => a.Paciente)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
