using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IExercicioService : IDisposable
    {
        Task Adicionar(Exercicio exercicio);

        Task Atualizar(Exercicio exercicio);

        Task Remover(Guid id);

        Task RemoverPorTreino(Guid treinoId);
    }
}
