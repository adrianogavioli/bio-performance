using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IGrupoAlimentoService : IDisposable
    {
        Task Adicionar(GrupoAlimento grupoAlimento);

        Task Atualizar(GrupoAlimento grupoAlimento);

        Task Remover(Guid id);
    }
}
