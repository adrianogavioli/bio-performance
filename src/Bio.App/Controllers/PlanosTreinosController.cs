using AutoMapper;
using Bio.App.ViewModels;
using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.App.Controllers
{
    public class PlanosTreinosController : BaseController
    {
        private readonly IPlanoTreinoRepository _planoTreinoRepository;
        private readonly IPlanoTreinoService _planoTreinoService;
        private readonly ITreinoRepository _treinoRepository;
        private readonly ITreinoService _treinoService;
        private readonly IExercicioRepository _exercicioRepository;
        private readonly IExercicioService _exercicioService;
        private readonly ITreinoRelGrupoMuscularService _treinoRelGrupoMuscularService;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IGrupoMuscularRepository _grupoMuscularRepository;
        private readonly ICatalogoExercicioRepository _catalogoExercicioRepository;
        private readonly IMapper _mapper;

        public PlanosTreinosController(
            IPlanoTreinoRepository planoTreinoRepository,
            IPlanoTreinoService planoTreinoService,
            ITreinoRepository treinoRepository,
            ITreinoService treinoService,
            IExercicioRepository exercicioRepository,
            IExercicioService exercicioService,
            ITreinoRelGrupoMuscularService treinoRelGrupoMuscularService,
            IAtendimentoRepository atendimentoRepository,
            IGrupoMuscularRepository grupoMuscularRepository,
            ICatalogoExercicioRepository catalogoExercicioRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _planoTreinoRepository = planoTreinoRepository;
            _planoTreinoService = planoTreinoService;
            _treinoRepository = treinoRepository;
            _treinoService = treinoService;
            _exercicioRepository = exercicioRepository;
            _exercicioService = exercicioService;
            _treinoRelGrupoMuscularService = treinoRelGrupoMuscularService;
            _atendimentoRepository = atendimentoRepository;
            _grupoMuscularRepository = grupoMuscularRepository;
            _catalogoExercicioRepository = catalogoExercicioRepository;
            _mapper = mapper;
        }

        [Route("planos-treinos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PlanoTreinoViewModel>>(await _planoTreinoRepository.ObterPlanosTreinos()));
        }

        [Route("plano-treino/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var planoTreinoViewModel = await ObterPlanoTreinoTreinos(id);

            if (planoTreinoViewModel == null) return NotFound();

            return View(planoTreinoViewModel);
        }

        [Route("imprimir-plano-treino/{id:guid}")]
        public async Task<IActionResult> Print(Guid id)
        {
            var planoTreinoViewModel = await ObterPlanoTreinoTreinos(id);

            if (planoTreinoViewModel == null) return NotFound();

            return View(planoTreinoViewModel);
        }

        [Route("adicionar-plano-treino/{atendimentoId:guid}")]
        public async Task<IActionResult> Create(Guid atendimentoId)
        {
            var atendimentoViewModel = await ObterAtendimento(atendimentoId);

            if (atendimentoViewModel == null) NotFound();

            var planoTreinoViewModel = new PlanoTreinoViewModel
            {
                AtendimentoId = atendimentoId,
                Atendimento = atendimentoViewModel
            };

            return View(planoTreinoViewModel);
        }

        [HttpPost]
        [Route("adicionar-plano-treino/{atendimentoId:guid}")]
        public async Task<IActionResult> Create(Guid atendimentoId, PlanoTreinoViewModel planoTreinoViewModel)
        {
            if (atendimentoId != planoTreinoViewModel.AtendimentoId) return NotFound();

            planoTreinoViewModel.Atendimento = await ObterAtendimento(atendimentoId);

            if (!ModelState.IsValid) return View(planoTreinoViewModel);

            var planoTreino = _mapper.Map<PlanoTreino>(planoTreinoViewModel);

            await _planoTreinoService.Adicionar(planoTreino);

            if (!OperacaoValida()) return View(planoTreinoViewModel);

            return RedirectToAction("Edit", new { id = planoTreino.Id });
        }

        [Route("editar-plano-treino/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var planoTreinoViewModel = await ObterPlanoTreinoTreinos(id);

            if (planoTreinoViewModel == null) return NotFound();

            return View(planoTreinoViewModel);
        }

        [HttpPost]
        [Route("editar-plano-treino/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, PlanoTreinoViewModel planoTreinoViewModel)
        {
            if (id != planoTreinoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(planoTreinoViewModel);

            var planoTreinoDb = await _planoTreinoRepository.ObterPorId(id);

            if (planoTreinoDb == null) return NotFound();

            planoTreinoDb.Observacao = planoTreinoViewModel.Observacao;
            planoTreinoDb.Ativo = planoTreinoViewModel.Ativo;

            await _planoTreinoService.Atualizar(planoTreinoDb);

            if (!OperacaoValida()) return View(planoTreinoViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-plano-treino/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var planoTreinoViewModel = await ObterPlanoTreino(id);

            if (planoTreinoViewModel == null) return NotFound();

            return View(planoTreinoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-plano-treino/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var planoTreinoViewModel = await ObterPlanoTreino(id);

            if (planoTreinoViewModel == null) return NotFound();

            await _planoTreinoService.Remover(id);

            if (!OperacaoValida()) return View(planoTreinoViewModel);

            return RedirectToAction("Index");
        }

        [Route("detalhar-treino-plano-treino/{id:guid}")]
        public async Task<IActionResult> DetalharTreino(Guid id)
        {
            var treinoViewModel = await ObterTreinoExercicios(id);

            if (treinoViewModel == null) return NotFound();

            return PartialView("_DetalharTreino", treinoViewModel);
        }

        [Route("adicionar-treino-plano-treino/{planoTreinoId:guid}")]
        public async Task<IActionResult> AdicionarTreino(Guid planoTreinoId)
        {
            var treinoViewModel = new TreinoViewModel
            {
                PlanoTreinoId = planoTreinoId
            };

            await PopularGruposMuscularesDropdown(treinoViewModel);

            return PartialView("_AdicionarTreino", treinoViewModel);
        }

        [HttpPost]
        [Route("adicionar-treino-plano-treino/{planoTreinoId:guid}")]
        public async Task<IActionResult> AdicionarTreino(Guid planoTreinoId, TreinoViewModel treinoViewModel)
        {
            if (planoTreinoId != treinoViewModel.PlanoTreinoId) return NotFound();

            if (!ModelState.IsValid) return PartialView("_AdicionarTreino", treinoViewModel);

            treinoViewModel.GruposMusculares = new List<TreinoRelGrupoMuscularViewModel>();

            foreach (var grupoMuscular in treinoViewModel.GruposMuscularesDropdown.Where(x => x.Selecionado))
            {
                treinoViewModel.GruposMusculares.Add(new TreinoRelGrupoMuscularViewModel
                {
                    GrupoMuscularId = grupoMuscular.Id
                });
            }

            var treino = _mapper.Map<Treino>(treinoViewModel);
            treino.Ativo = true;

            await _treinoService.Adicionar(treino);

            if (!OperacaoValida()) return PartialView("_AdicionarTreino", treinoViewModel);

            var url = Url.Action("ObterTreinosPorPlanoTreino", "PlanosTreinos", new { planoTreinoId });
            return Json(new { success = true, url });
        }

        [Route("editar-treino-plano-treino/{id:guid}")]
        public async Task<IActionResult> EditarTreino(Guid id)
        {
            var treinoViewModel = await ObterTreinoExercicios(id);

            if (treinoViewModel == null) return NotFound();

            return PartialView("_EditarTreino", treinoViewModel);
        }

        [HttpPost]
        [Route("editar-treino-plano-treino/{id:guid}")]
        public async Task<IActionResult> EditarTreino(Guid id, TreinoViewModel treinoViewModel)
        {
            if (id != treinoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return PartialView("_EditarTreino", treinoViewModel);

            var treinoDb = await ObterTreino(id);

            if (treinoDb == null)
            {
                ModelState.AddModelError(string.Empty, "O treino selecionado pode ter sido removido, favor verificar.");
                return PartialView("_EditarTreino", treinoViewModel);
            }

            treinoDb.Ordem = treinoViewModel.Ordem;
            treinoDb.Titulo = treinoViewModel.Titulo;
            treinoDb.Observacao = treinoViewModel.Observacao;
            treinoDb.Ativo = treinoViewModel.Ativo;

            await _treinoService.Atualizar(_mapper.Map<Treino>(treinoDb));

            if (!OperacaoValida()) return PartialView("_EditarTreino", treinoViewModel);

            var url = Url.Action("ObterTreinosPorPlanoTreino", "PlanosTreinos", new { planoTreinoId = treinoDb.PlanoTreinoId });
            return Json(new { success = true, url });
        }

        [Route("remover-treino-plano-treino/{id:guid}")]
        public async Task<IActionResult> RemoverTreino(Guid id)
        {
            var treinoViewModel = await ObterTreino(id);

            if (treinoViewModel == null) return NotFound();

            return PartialView("_RemoverTreino", treinoViewModel);
        }

        [HttpPost, ActionName("RemoverTreino")]
        [Route("remover-treino-plano-treino/{id:guid}")]
        public async Task<IActionResult> RemoverTreinoConfirmed(Guid id)
        {
            var treinoViewModel = await ObterTreino(id);

            if (treinoViewModel == null) return NotFound();

            await _treinoService.Remover(id);

            if (!OperacaoValida()) return PartialView("_RemoverTreino", treinoViewModel);

            var url = Url.Action("ObterTreinosPorPlanoTreino", "PlanosTreinos", new { planoTreinoId = treinoViewModel.PlanoTreinoId });
            return Json(new { success = true, url });
        }

        [Route("gerenciar-exercicio-treino/{treinoId:guid}")]
        public async Task<IActionResult> GerenciarExercicio(Guid treinoId)
        {
            var treinoViewModel = await ObterTreinoExercicios(treinoId);

            if (treinoViewModel == null) return NotFound();

            return PartialView("_GerenciarExercicios", treinoViewModel);
        }

        [Route("adicionar-exercicio-treino/{treinoId:guid}")]
        public async Task<IActionResult> AdicionarExercicio(Guid treinoId)
        {
            var treinoViewModel = await ObterTreino(treinoId);

            if (treinoViewModel == null) return NotFound();

            var exercicioViewModel = new ExercicioViewModel
            {
                TreinoId = treinoId,
                Treino = treinoViewModel
            };

            await PopularCatalogoExerciciosDropdown(exercicioViewModel);

            return PartialView("_AdicionarExercicio", exercicioViewModel);
        }

        [HttpPost]
        [Route("adicionar-exercicio-treino/{treinoId:guid}")]
        public async Task<IActionResult> AdicionarExercicio(Guid treinoId, ExercicioViewModel exercicioViewModel)
        {
            if (treinoId != exercicioViewModel.TreinoId) return NotFound();

            var treinoViewModel = await ObterTreino(treinoId);

            if (treinoViewModel == null) return NotFound();

            exercicioViewModel.Treino = treinoViewModel;

            await PopularCatalogoExerciciosDropdown(exercicioViewModel);

            if (!ModelState.IsValid) return PartialView("_AdicionarExercicio", exercicioViewModel);

            await _exercicioService.Adicionar(_mapper.Map<Exercicio>(exercicioViewModel));

            if (!OperacaoValida()) return PartialView("_AdicionarExercicio", exercicioViewModel);

            return PartialView("_GerenciarExercicios", await ObterTreinoExercicios(treinoId));
        }

        [Route("editar-exercicio-treino/{id:guid}")]
        public async Task<IActionResult> EditarExercicio(Guid id)
        {
            var exercicioViewModel = await ObterExercicio(id);

            if (exercicioViewModel == null) return NotFound();

            return PartialView("_EditarExercicio", exercicioViewModel);
        }

        [HttpPost]
        [Route("editar-exercicio-treino/{id:guid}")]
        public async Task<IActionResult> EditarExercicio(Guid id, ExercicioViewModel exercicioViewModel)
        {
            if (id != exercicioViewModel.Id) return NotFound();

            var exercicioDb = await ObterExercicio(id);

            exercicioViewModel.Treino = exercicioDb.Treino;
            exercicioViewModel.CatalogoExercicio = exercicioDb.CatalogoExercicio;

            if (!ModelState.IsValid) return PartialView("_EditarExercicio", exercicioViewModel);

            exercicioDb.Ordem = exercicioViewModel.Ordem;
            exercicioDb.Repeticao = exercicioViewModel.Repeticao;
            exercicioDb.Observacao = exercicioViewModel.Observacao;

            await _exercicioService.Atualizar(_mapper.Map<Exercicio>(exercicioDb));

            if (!OperacaoValida()) return PartialView("_EditarExercicio", exercicioViewModel);

            return PartialView("_GerenciarExercicios", await ObterTreinoExercicios(exercicioDb.TreinoId));
        }

        [Route("remover-exercicio-treino/{id:guid}")]
        public async Task<IActionResult> RemoverExercicio(Guid id)
        {
            var exercicioViewModel = await ObterExercicio(id);

            if (exercicioViewModel == null) return NotFound();

            return PartialView("_RemoverExercicio", exercicioViewModel);
        }

        [HttpPost, ActionName("RemoverExercicio")]
        [Route("remover-exercicio-treino/{id:guid}")]
        public async Task<IActionResult> RemoverExercicioConfirmed(Guid id)
        {
            var exercicioViewModel = await ObterExercicio(id);

            if (exercicioViewModel == null) return NotFound();

            await _exercicioService.Remover(id);

            if (!OperacaoValida()) return PartialView("_RemoverExercicio", exercicioViewModel);

            return PartialView("_GerenciarExercicios", await ObterTreinoExercicios(exercicioViewModel.TreinoId));
        }

        private async Task<PlanoTreinoViewModel> ObterPlanoTreino(Guid id)
        {
            return _mapper.Map<PlanoTreinoViewModel>(await _planoTreinoRepository.ObterPlanoTreino(id));
        }

        private async Task<PlanoTreinoViewModel> ObterPlanoTreinoTreinos(Guid id)
        {
            var planoTreino = await _planoTreinoRepository.ObterPlanoTreinoTreinos(id);

            planoTreino.Treinos = planoTreino.Treinos.OrderBy(t => t.Ordem);

            var planoTreinoViewModel = _mapper.Map<PlanoTreinoViewModel>(planoTreino);

            foreach (var treino in planoTreinoViewModel.Treinos)
            {
                await PreencherGruposMuscularesConcats(treino);
            }

            return planoTreinoViewModel;
        }

        private async Task<TreinoViewModel> ObterTreino(Guid id)
        {
            var treinoViewModel = _mapper.Map<TreinoViewModel>(await _treinoRepository.ObterTreino(id));

            await PreencherGruposMuscularesConcats(treinoViewModel);

            return treinoViewModel;
        }

        private async Task<TreinoViewModel> ObterTreinoExercicios(Guid id)
        {
            var treino = await _treinoRepository.ObterTreinoExercicios(id);

            treino.Exercicios = treino.Exercicios.OrderBy(t => t.Ordem);

            var treinoViewModel = _mapper.Map<TreinoViewModel>(treino);

            await PreencherGruposMuscularesConcats(treinoViewModel);

            return treinoViewModel;
        }

        public async Task<IActionResult> ObterTreinosPorPlanoTreino(Guid planoTreinoId)
        {
            var planoTreinoViewModel = await ObterPlanoTreinoTreinos(planoTreinoId);

            if (planoTreinoViewModel == null) return NotFound();

            return PartialView("_Treinos", planoTreinoViewModel);
        }

        private async Task<ExercicioViewModel> ObterExercicio(Guid id)
        {
            var exercicioViewModel = _mapper.Map<ExercicioViewModel>(await _exercicioRepository.ObterExercicio(id));

            await PreencherGruposMuscularesConcats(exercicioViewModel.Treino);

            return exercicioViewModel;
        }

        private async Task<AtendimentoViewModel> ObterAtendimento(Guid atendimentoId)
        {
            return _mapper.Map<AtendimentoViewModel>(await _atendimentoRepository.ObterAtendimento(atendimentoId));
        }

        private async Task<TreinoViewModel> PopularGruposMuscularesDropdown(TreinoViewModel treinoViewModel)
        {
            var gruposMusculares = await _grupoMuscularRepository.Buscar(r => r.Ativo);

            treinoViewModel.GruposMuscularesDropdown = _mapper.Map<List<GrupoMuscularViewModel>>(gruposMusculares.OrderBy(r => r.Descricao));

            return treinoViewModel;
        }

        private async Task<ExercicioViewModel> PopularCatalogoExerciciosDropdown(ExercicioViewModel exercicioViewModel)
        {
            var catalogoExercicios = new List<CatalogoExercicio>();

            foreach (var grupoMuscular in exercicioViewModel.Treino.GruposMusculares)
            {
                catalogoExercicios.AddRange(await _catalogoExercicioRepository.Buscar(c => c.GrupoMuscularId == grupoMuscular.GrupoMuscularId && c.Ativo));
            }

            exercicioViewModel.CatalogoExerciciosDropdown = _mapper.Map<List<CatalogoExercicioViewModel>>(catalogoExercicios.OrderBy(c => c.Descricao));

            return exercicioViewModel;
        }

        private async Task<TreinoViewModel> PreencherGruposMuscularesConcats(TreinoViewModel treinoViewModel)
        {
            treinoViewModel.GruposMuscularesConcats = string.Join(", ", treinoViewModel.GruposMusculares.Select(g => g.GrupoMuscular.Descricao));

            return await Task.FromResult(treinoViewModel);
        }
    }
}