using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Repositories
{
    public interface IDietaRefeicaoAlimentoRepository : IRepository<DietaRefeicaoAlimento>
    {
        Task<DietaRefeicaoAlimento> ObterDietaRefeicaoAlimento(Guid id);

        Task<DietaRefeicaoAlimento> ObterDietaRefeicaoAlimentoSubstituicoes(Guid id);

        Task RemoverPorDieta(Guid dietaId);
    }
}
