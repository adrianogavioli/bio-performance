using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IGrupoMuscularService : IDisposable
    {
        Task Adicionar(GrupoMuscular grupoMuscular);

        Task Atualizar(GrupoMuscular grupoMuscular);

        Task Remover(Guid id);
    }
}
