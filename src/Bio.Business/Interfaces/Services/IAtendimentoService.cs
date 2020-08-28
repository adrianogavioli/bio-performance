using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IAtendimentoService : IDisposable
    {
        Task Adicionar(Atendimento atendimento);

        Task Atualizar(Atendimento atendimento);

        Task Remover(Guid id);
    }
}
