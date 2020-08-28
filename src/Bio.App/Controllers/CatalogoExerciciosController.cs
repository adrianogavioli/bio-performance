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
    public class CatalogoExerciciosController : BaseController
    {
        private readonly ICatalogoExercicioRepository _catalogoExercicioRepository;
        private readonly ICatalogoExercicioService _catalogoExercicioService;
        private readonly IGrupoMuscularRepository _grupoMuscularRepository;
        private readonly IMapper _mapper;

        public CatalogoExerciciosController(
            ICatalogoExercicioRepository catalogoExercicioRepository,
            ICatalogoExercicioService catalogoExercicioService,
            IGrupoMuscularRepository grupoMuscularRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _catalogoExercicioRepository = catalogoExercicioRepository;
            _catalogoExercicioService = catalogoExercicioService;
            _grupoMuscularRepository = grupoMuscularRepository;
            _mapper = mapper;
        }

        [Route("catalogo-exercicios")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CatalogoExercicioViewModel>>(await _catalogoExercicioRepository.ObterCatalogoExercicios()));
        }

        [Route("catalogo-exercicio/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var catalogoExercicioViewModel = await ObterCatalogoExercicio(id);

            if (catalogoExercicioViewModel == null) return NotFound();

            return View(catalogoExercicioViewModel);
        }

        [Route("adicionar-catalogo-exercicio")]
        public async Task<IActionResult> Create()
        {
            var catalogoExercicioViewModel = new CatalogoExercicioViewModel();

            await PopularGruposMuscularesDropdown(catalogoExercicioViewModel);

            return View(catalogoExercicioViewModel);
        }

        [HttpPost]
        [Route("adicionar-catalogo-exercicio")]
        public async Task<IActionResult> Create(CatalogoExercicioViewModel catalogoExercicioViewModel)
        {
            await PopularGruposMuscularesDropdown(catalogoExercicioViewModel);

            if (!ModelState.IsValid) return View(catalogoExercicioViewModel);

            catalogoExercicioViewModel.Ativo = true;

            await _catalogoExercicioService.Adicionar(_mapper.Map<CatalogoExercicio>(catalogoExercicioViewModel));

            if (!OperacaoValida()) return View(catalogoExercicioViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-catalogo-exercicio/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var catalogoExercicioViewModel = await ObterCatalogoExercicio(id);

            if (catalogoExercicioViewModel == null) return NotFound();

            return View(catalogoExercicioViewModel);
        }

        [HttpPost]
        [Route("editar-catalogo-exercicio/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id, CatalogoExercicioViewModel catalogoExercicioViewModel)
        {
            if (id != catalogoExercicioViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(catalogoExercicioViewModel);

            var catalogoExercicioDb = await ObterCatalogoExercicio(id);

            if (catalogoExercicioDb == null) return NotFound();

            catalogoExercicioDb.Descricao = catalogoExercicioViewModel.Descricao;
            catalogoExercicioDb.Ativo = catalogoExercicioViewModel.Ativo;

            await _catalogoExercicioService.Atualizar(_mapper.Map<CatalogoExercicio>(catalogoExercicioDb));

            if (!OperacaoValida()) return View(catalogoExercicioViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-catalogo-exercicio/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var catalogoExercicioViewModel = await ObterCatalogoExercicio(id);

            if (catalogoExercicioViewModel == null) return NotFound();

            return View(catalogoExercicioViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-catalogo-exercicio/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var catalogoExercicioViewModel = await ObterCatalogoExercicio(id);

            if (catalogoExercicioViewModel == null) return NotFound();

            await _catalogoExercicioService.Remover(id);

            if (!OperacaoValida()) return View(catalogoExercicioViewModel);

            return RedirectToAction("Index");
        }

        private async Task<CatalogoExercicioViewModel> ObterCatalogoExercicio(Guid id)
        {
            return _mapper.Map<CatalogoExercicioViewModel>(await _catalogoExercicioRepository.ObterCatalogoExercicio(id));
        }

        private async Task<CatalogoExercicioViewModel> PopularGruposMuscularesDropdown(CatalogoExercicioViewModel catalogoExercicioViewModel)
        {
            var gruposMuscularesViewModel = _mapper.Map<IEnumerable<GrupoMuscularViewModel>>(await _grupoMuscularRepository.ObterTodos());

            catalogoExercicioViewModel.GruposMuscularesDropdown = gruposMuscularesViewModel.OrderBy(g => g.Descricao).ToList();

            return catalogoExercicioViewModel;
        }
    }
}
