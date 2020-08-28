using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IBioImpedanciaService : IDisposable
    {
        Task Adicionar(BioImpedancia bioImpedancia);

        Task Atualizar(BioImpedancia bioImpedancia);

        Task Remover(Guid id);
    }
}
