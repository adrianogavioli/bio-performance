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
    public class DiarioPacienteRepository : Repository<DiarioPaciente>, IDiarioPacienteRepository
    {
        public DiarioPacienteRepository(BioDbContext context) : base(context) {}

        public async Task<DiarioPaciente> ObterDiarioPaciente(Guid id)
        {
            return await _db.DiariosPacientes
                .AsNoTracking()
                .Include(d => d.Paciente)
                .Include(d => d.Treino)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DiarioPaciente>> ObterDiariosPacientes()
        {
            return await _db.DiariosPacientes
                .AsNoTracking()
                .Include(d => d.Paciente)
                .Include(d => d.Treino)
                .ToListAsync();
        }

        public async Task<IEnumerable<DiarioPaciente>> ObterDiariosPacientesPorPaciente(Guid pacienteId)
        {
            return await _db.DiariosPacientes
                .AsNoTracking()
                .Include(d => d.Treino)
                .Where(d => d.PacienteId == pacienteId)
                .ToListAsync();
        }
    }
}
