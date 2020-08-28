using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IDietaService : IDisposable
    {
        Task Adicionar(Dieta dieta);

        Task Atualizar(Dieta dieta);

        Task Remover(Guid id);
    }
}
