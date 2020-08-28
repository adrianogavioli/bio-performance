using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IRefeicaoService : IDisposable
    {
        Task Adicionar(Refeicao refeicao);

        Task Atualizar(Refeicao refeicao);

        Task Remover(Guid id);
    }
}
