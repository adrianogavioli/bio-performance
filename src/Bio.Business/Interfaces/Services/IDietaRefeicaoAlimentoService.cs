using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IDietaRefeicaoAlimentoService : IDisposable
    {
        Task Adicionar(DietaRefeicaoAlimento dietaRefeicaoAlimento);

        Task Atualizar(DietaRefeicaoAlimento dietaRefeicaoAlimento);

        Task Remover(Guid id);

        Task RemoverPorDieta(Guid dietaId);
    }
}
