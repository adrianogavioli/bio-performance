using Bio.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IAlimentoSubstituicaoRepository : IRepository<AlimentoSubstituicao>
    {
        Task<AlimentoSubstituicao> ObterAlimentoSubstituicao(Guid id);

        Task<IEnumerable<AlimentoSubstituicao>> ObterAlimentosSubstituicoesPorAlimentoId(Guid alimentoId);

        Task RemoverPorAlimento(Guid alimentoId);
    }
}
