using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IPacienteService : IDisposable
    {
        Task Adicionar(Paciente paciente);

        Task Atualizar(Paciente paciente);

        Task Remover(Guid id);
    }
}
