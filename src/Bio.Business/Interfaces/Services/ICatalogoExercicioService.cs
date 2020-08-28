using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface ICatalogoExercicioService : IDisposable
    {
        Task Adicionar(CatalogoExercicio catalogoExercicio);

        Task Atualizar(CatalogoExercicio catalogoExercicio);

        Task Remover(Guid id);
    }
}
