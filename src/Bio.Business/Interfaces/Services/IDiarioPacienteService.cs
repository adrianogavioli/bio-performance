using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IDiarioPacienteService : IDisposable
    {
        Task Adicionar(DiarioPaciente diarioPaciente);

        Task Remover(Guid id);
    }
}
