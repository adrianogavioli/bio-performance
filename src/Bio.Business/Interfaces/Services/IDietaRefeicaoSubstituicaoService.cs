using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IDietaRefeicaoSubstituicaoService : IDisposable
    {
        Task Adicionar(DietaRefeicaoSubstituicao dietaRefeicaoSubstituicao);

        Task Atualizar(DietaRefeicaoSubstituicao dietaRefeicaoSubstituicao);

        Task Remover(Guid id);

        Task RemoverPorRefeicao(Guid dietaRefeicaoAlimentoId);
    }
}
