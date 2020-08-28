using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IDietaRepository : IRepository<Dieta>
    {
        Task<Dieta> ObterDieta(Guid id);

        Task<Dieta> ObterDietaRefeicoesAlimentos(Guid id);

        Task<Dieta> ObterDietaRefeicoesSubstituicoes(Guid id);

        Task<IEnumerable<Dieta>> ObterDietas();

        Task<IEnumerable<Dieta>> ObterDietasPorAtendimento(Guid atendimentoId);
    }
}
