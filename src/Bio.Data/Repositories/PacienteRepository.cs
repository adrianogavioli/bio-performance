using Bio.Business.Interfaces.Repositories;
using Bio.Business.Models;
using Bio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bio.Data.Repositories
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(BioDbContext context) : base(context) { }

        public async Task<Paciente> ObterPaciente(Guid id)
        {
            return await _db.Pacientes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente> ObterPacienteBioImpedancias(Guid id)
        {
            return await _db.Pacientes
                .AsNoTracking()
                .Include(p => p.BioImpedancias)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente> ObterPacienteAtendimentos(Guid id)
        {
            return await _db.Pacientes
                .AsNoTracking()
                .Include(p => p.Atendimentos)
                    .ThenInclude(a => a.Profissional)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente> ObterPacienteDiarios(Guid id)
        {
            return await _db.Pacientes
                .AsNoTracking()
                .Include(p => p.Diarios)
                    .ThenInclude(d => d.Treino)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente> ObterPacienteBioImpedanciasAtendimentosDiarios(Guid id)
        {
            return await _db.Pacientes
                .AsNoTracking()
                .Include(p => p.BioImpedancias)
                .Include(p => p.Atendimentos)
                    .ThenInclude(a => a.Profissional)
                .Include(p => p.Diarios)
                    .ThenInclude(d => d.Treino)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
