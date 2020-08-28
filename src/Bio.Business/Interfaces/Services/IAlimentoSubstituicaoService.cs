using Bio.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bio.Business.Interfaces.Services
{
    public interface IAlimentoSubstituicaoService : IDisposable
    {
        Task Adicionar(AlimentoSubstituicao alimentoSubstituicao);

        Task Remover(Guid id);

        Task RemoverPorAlimento(Guid alimentoId);
    }
}
