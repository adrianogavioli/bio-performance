using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IDietaRefeicaoSubstituicaoRepository : IRepository<DietaRefeicaoSubstituicao>
    {
        Task<DietaRefeicaoSubstituicao> ObterDietaRefeicaoSubstituicao(Guid id);

        Task RemoverPorRefeicao(Guid dietaRefeicaoAlimentoId);
    }
}
