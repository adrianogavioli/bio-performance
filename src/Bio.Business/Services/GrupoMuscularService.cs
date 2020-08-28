using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Bio.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.Business.Services
{
    public class GrupoMuscularService : BaseService, IGrupoMuscularService
    {
        private readonly IGrupoMuscularRepository _grupoMuscularRepository;
        private readonly ITreinoRelGrupoMuscularRepository _treinoRelGrupoMuscularRepository;
        private readonly ICatalogoExercicioRepository _catalogoExercicioRepository;

        public GrupoMuscularService(
            IGrupoMuscularRepository grupoMuscularRepository,
            ITreinoRelGrupoMuscularRepository treinoRelGrupoMuscularRepository,
            ICatalogoExercicioRepository catalogoExercicioRepository,
            INotificador notificador) : base(notificador)
        {
            _grupoMuscularRepository = grupoMuscularRepository;
            _treinoRelGrupoMuscularRepository = treinoRelGrupoMuscularRepository;
            _catalogoExercicioRepository = catalogoExercicioRepository;
        }

        public async Task Adicionar(GrupoMuscular grupoMuscular)
        {
            if (!ExecutarValidacao(new GrupoMuscularValidation(), grupoMuscular)) return;

            if (_grupoMuscularRepository.Buscar(g => g.Descricao == grupoMuscular.Descricao).Result.Any())
            {
                Notificar("A descrição informada já existe na lista de grupos musculares.");
                return;
            }

            await _grupoMuscularRepository.Adicionar(grupoMuscular);
        }

        public async Task Atualizar(GrupoMuscular grupoMuscular)
        {
            if (!ExecutarValidacao(new GrupoMuscularValidation(), grupoMuscular)) return;

            if (_grupoMuscularRepository.Buscar(g => g.Descricao == grupoMuscular.Descricao && g.Id != grupoMuscular.Id).Result.Any())
            {
                Notificar("A descrição informada já existe na lista de grupos musculares.");
                return;
            }

            await _grupoMuscularRepository.Atualizar(grupoMuscular);
        }

        public async Task Remover(Guid id)
        {
            if (_treinoRelGrupoMuscularRepository.Buscar(r => r.GrupoMuscularId == id).Result.Any()
                || _catalogoExercicioRepository.Buscar(c => c.GrupoMuscularId == id).Result.Any())
            {
                Notificar("Este grupo muscular está sendo utilizado, portanto não pode ser removido. Considere inativa-lo.");
                return;
            }

            await _grupoMuscularRepository.Remover(id);
        }

        public void Dispose()
        {
            _grupoMuscularRepository?.Dispose();
            _treinoRelGrupoMuscularRepository?.Dispose();
            _catalogoExercicioRepository?.Dispose();
        }
    }
}
