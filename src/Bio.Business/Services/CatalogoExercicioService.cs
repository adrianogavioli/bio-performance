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
    public class CatalogoExercicioService : BaseService, ICatalogoExercicioService
    {
        private readonly ICatalogoExercicioRepository _catalogoExercicioRepository;
        private readonly IExercicioRepository _exercicioRepository;

        public CatalogoExercicioService(
            ICatalogoExercicioRepository catalogoExercicioRepository,
            IExercicioRepository exercicioRepository,
            INotificador notificador) : base(notificador)
        {
            _catalogoExercicioRepository = catalogoExercicioRepository;
            _exercicioRepository = exercicioRepository;
        }

        public async Task Adicionar(CatalogoExercicio catalogoExercicio)
        {
            if (!ExecutarValidacao(new CatalogoExercicioValidation(), catalogoExercicio)) return;

            if (_catalogoExercicioRepository.Buscar(c => c.Descricao == catalogoExercicio.Descricao).Result.Any())
            {
                Notificar("A descrição informada já existe no catálogo de exercícios.");
                return;
            }

            await _catalogoExercicioRepository.Adicionar(catalogoExercicio);
        }

        public async Task Atualizar(CatalogoExercicio catalogoExercicio)
        {
            if (!ExecutarValidacao(new CatalogoExercicioValidation(), catalogoExercicio)) return;

            if (_catalogoExercicioRepository.Buscar(c => c.Descricao == catalogoExercicio.Descricao && c.Id != catalogoExercicio.Id).Result.Any())
            {
                Notificar("A descrição informada já existe no catálogo de exercícios.");
                return;
            }

            await _catalogoExercicioRepository.Atualizar(catalogoExercicio);
        }

        public async Task Remover(Guid id)
        {
            if (_exercicioRepository.Buscar(e => e.CatalogoExercicioId == id).Result.Any())
            {
                Notificar("Este exercício está sendo utilizado, portanto não pode ser removido. Considere inativa-lo.");
                return;
            }

            await _catalogoExercicioRepository.Remover(id);
        }

        public void Dispose()
        {
            _catalogoExercicioRepository?.Dispose();
            _exercicioRepository?.Dispose();
        }
    }
}
