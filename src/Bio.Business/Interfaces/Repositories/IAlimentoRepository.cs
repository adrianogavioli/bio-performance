using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IAlimentoRepository : IRepository<Alimento>
    {
        Task<Alimento> ObterAlimento(Guid id);

        Task<Alimento> ObterAlimentoSubstituicoes(Guid id);

        Task<IEnumerable<Alimento>> ObterAlimentos();

        Task<IEnumerable<Alimento>> ObterAlimentosPorGrupoAlimento(Guid grupoAlimentoId);

        Task<Alimento> ObterAlimentoPorDescricao(string descricao);
    }
}
