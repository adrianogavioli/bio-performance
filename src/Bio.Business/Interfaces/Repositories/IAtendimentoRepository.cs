using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IAtendimentoRepository : IRepository<Atendimento>
    {
        Task<Atendimento> ObterAtendimento(Guid id);

        Task<Atendimento> ObterAtendimentoPlanosTreinos(Guid id);

        Task<Atendimento> ObterAtendimentoDietas(Guid id);

        Task<Atendimento> ObterAtendimentoPlanosTreinosDietas(Guid id);

        Task<IEnumerable<Atendimento>> ObterAtendimentosPorProfissional(Guid profissionalId);

        Task<IEnumerable<Atendimento>> ObterAtendimentosPorPaciente(Guid pacienteId);

        Task<IEnumerable<Atendimento>> ObterAtendimentos();
    }
}
