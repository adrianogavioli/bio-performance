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
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository(BioDbContext context) : base(context) {}

        public async Task<Atendimento> ObterAtendimento(Guid id)
        {
            return await _db.Atendimentos
                .AsNoTracking()
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Atendimento> ObterAtendimentoPlanosTreinos(Guid id)
        {
            return await _db.Atendimentos
                .AsNoTracking()
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Include(a => a.PlanosTreinos)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Atendimento> ObterAtendimentoDietas(Guid id)
        {
            return await _db.Atendimentos
                .AsNoTracking()
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Include(a => a.Dietas)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Atendimento> ObterAtendimentoPlanosTreinosDietas(Guid id)
        {
            return await _db.Atendimentos
                .AsNoTracking()
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Include(a => a.PlanosTreinos)
                .Include(a => a.Dietas)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Atendimento>> ObterAtendimentosPorProfissional(Guid profissionalId)
        {
            return await _db.Atendimentos
                .AsNoTracking()
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Where(a => a.ProfissionalId == profissionalId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Atendimento>> ObterAtendimentosPorPaciente(Guid pacienteId)
        {
            return await _db.Atendimentos
                .AsNoTracking()
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .Where(a => a.PacienteId == pacienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Atendimento>> ObterAtendimentos()
        {
            return await _db.Atendimentos
                .AsNoTracking()
                .Include(a => a.Paciente)
                .Include(a => a.Profissional)
                .ToListAsync();
        }
    }
}
