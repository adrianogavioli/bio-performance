using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface ITreinoRelGrupoMuscularService : IDisposable
    {
        Task Adicionar(TreinoRelGrupoMuscular treinoRelGrupoMuscular);

        Task Remover(Guid id);

        Task RemoverPorTreino(Guid treinoId);
    }
}
