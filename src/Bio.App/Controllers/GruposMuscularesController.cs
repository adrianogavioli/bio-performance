using AutoMapper;
using Bio.App.ViewModels;
using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.App.Controllers
{
    public class GruposMuscularesController : BaseController
    {
        private readonly IGrupoMuscularRepository _grupoMuscularRepository;
        private readonly IGrupoMuscularService _grupoMuscularService;
        private readonly IMapper _mapper;

        public GruposMuscularesController(
            IGrupoMuscularRepository grupoMuscularRepository,
            IGrupoMuscularService grupoMuscularService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _grupoMuscularRepository = grupoMuscularRepository;
            _grupoMuscularService = grupoMuscularService;
            _mapper = mapper;
        }

        [Route("grupos-musculares")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GrupoMuscularViewModel>>(await _grupoMuscularRepository.ObterTodos()));
        }

        [Route("grupo-muscular/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var grupoMuscularViewModel = await ObterGrupoMuscular(id);

            if (grupoMuscularViewModel == null) return NotFound();

            return View(grupoMuscularViewModel);
        }

        [Route("adicionar-grupo-muscular")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("adicionar-grupo-muscular")]
        public async Task<IActionResult> Create(GrupoMuscularViewModel grupoMuscularViewModel)
        {
            if (!ModelState.IsValid) return View(grupoMuscularViewModel);

            grupoMuscularViewModel.Ativo = true;

            await _grupoMuscularService.Adicionar(_mapper.Map<GrupoMuscular>(grupoMuscularViewModel));

            if (!OperacaoValida()) return View(grupoMuscularViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-grupo-muscular/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var grupoMuscularViewModel = await ObterGrupoMuscular(id);

            if (grupoMuscularViewModel == null) return NotFound();

            return View(grupoMuscularViewModel);
        }

        [HttpPost]
        [Route("editar-grupo-muscular/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id, GrupoMuscularViewModel grupoMuscularViewModel)
        {
            if (id != grupoMuscularViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(grupoMuscularViewModel);

            var grupoMuscularDb = await ObterGrupoMuscular(id);

            if (grupoMuscularDb == null) return NotFound();

            grupoMuscularDb.Descricao = grupoMuscularViewModel.Descricao;
            grupoMuscularDb.Ativo = grupoMuscularViewModel.Ativo;

            await _grupoMuscularService.Atualizar(_mapper.Map<GrupoMuscular>(grupoMuscularDb));

            if (!OperacaoValida()) return View(grupoMuscularViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-grupo-muscular/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var grupoMuscularViewModel = await ObterGrupoMuscular(id);

            if (grupoMuscularViewModel == null) return NotFound();

            return View(grupoMuscularViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-grupo-muscular/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grupoMuscularViewModel = await ObterGrupoMuscular(id);

            if (grupoMuscularViewModel == null) return NotFound();

            await _grupoMuscularService.Remover(id);

            if (!OperacaoValida()) return View(grupoMuscularViewModel);

            return RedirectToAction("Index");
        }

        private async Task<GrupoMuscularViewModel> ObterGrupoMuscular(Guid id)
        {
            return _mapper.Map<GrupoMuscularViewModel>(await _grupoMuscularRepository.ObterGrupoMuscular(id));
        }
    }
}
