using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IProfissionalService : IDisposable
    {
        Task Adicionar(Profissional profissional);

        Task Atualizar(Profissional profissional);

        Task Remover(Guid id);
    }
}
