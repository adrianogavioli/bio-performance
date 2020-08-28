using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IUnidadeMedidaService : IDisposable
    {
        Task Adicionar(UnidadeMedida unidadeMedida);

        Task Atualizar(UnidadeMedida unidadeMedida);

        Task Remover(Guid id);
    }
}