using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IPlanoTreinoService : IDisposable
    {
        Task Adicionar(PlanoTreino planoTreino);

        Task Atualizar(PlanoTreino planoTreino);

        Task Remover(Guid id);
    }
}
