using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IAlimentoService : IDisposable
    {
        Task Adicionar(Alimento alimento);

        Task Atualizar(Alimento alimento);

        Task Remover(Guid id);
    }
}
