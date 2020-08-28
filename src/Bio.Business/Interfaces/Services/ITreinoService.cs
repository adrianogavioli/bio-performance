using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface ITreinoService : IDisposable
    {
        Task Adicionar(Treino treino);

        Task Atualizar(Treino treino);

        Task Remover(Guid id);
    }
}
