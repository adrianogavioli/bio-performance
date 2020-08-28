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
    public class ExercicioService : BaseService, IExercicioService
    {
        private readonly IExercicioRepository _exercicioRepository;

        public ExercicioService(
            IExercicioRepository exercicioRepository,
            INotificador notificador) : base(notificador)
        {
            _exercicioRepository = exercicioRepository;
        }

        public async Task Adicionar(Exercicio exercicio)
        {
            if (!ExecutarValidacao(new ExercicioValidation(), exercicio)) return;

            if (_exercicioRepository.Buscar(e => e.TreinoId == exercicio.TreinoId && e.Ordem == exercicio.Ordem).Result.Any())
            {
                Notificar("A ordem informada no exercício já existe nesse treino.");
                return;
            }

            exercicio.Treino = null;

            await _exercicioRepository.Adicionar(exercicio);
        }

        public async Task Atualizar(Exercicio exercicio)
        {
            if (!ExecutarValidacao(new ExercicioValidation(), exercicio)) return;

            if (_exercicioRepository.Buscar(e => e.TreinoId == exercicio.TreinoId && e.Ordem == exercicio.Ordem && e.Id != exercicio.Id).Result.Any())
            {
                Notificar("A ordem informada no exercício já existe nesse treino.");
                return;
            }

            exercicio.Treino = null;

            await _exercicioRepository.Atualizar(exercicio);
        }

        public async Task Remover(Guid id)
        {
            await _exercicioRepository.Remover(id);
        }

        public async Task RemoverPorTreino(Guid treinoId)
        {
            var exercicios = await _exercicioRepository.Buscar(e => e.TreinoId == treinoId);

            foreach (var exercicio in exercicios)
            {
                await Remover(exercicio.Id);
            }
        }

        public void Dispose()
        {
            _exercicioRepository?.Dispose();
        }
    }
}
