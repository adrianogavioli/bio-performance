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
    public class GruposAlimentosController : BaseController
    {
        private readonly IGrupoAlimentoRepository _grupoAlimentoRepository;
        private readonly IGrupoAlimentoService _grupoAlimentoService;
        private readonly IMapper _mapper;

        public GruposAlimentosController(IGrupoAlimentoRepository grupoAlimentoRepository,
            IGrupoAlimentoService grupoAlimentoService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _grupoAlimentoRepository = grupoAlimentoRepository;
            _grupoAlimentoService = grupoAlimentoService;
            _mapper = mapper;
        }

        [Route("grupos-alimentos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GrupoAlimentoViewModel>>(await _grupoAlimentoRepository.ObterTodos()));
        }

        [Route("grupo-alimento/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var grupoAlimentoViewModel = await ObterGrupoAlimento(id);

            if (grupoAlimentoViewModel == null) return NotFound();

            return View(grupoAlimentoViewModel);
        }

        [Route("adicionar-grupo-alimento")]
        public IActionResult Create()
        {
            return View(new GrupoAlimentoViewModel
            {
                Ativo = true
            });
        }

        [HttpPost]
        [Route("adicionar-grupo-alimento")]
        public async Task<IActionResult> Create(GrupoAlimentoViewModel grupoAlimentoViewModel)
        {
            grupoAlimentoViewModel.Ativo = true;

            if (!ModelState.IsValid) return View(grupoAlimentoViewModel);

            await _grupoAlimentoService.Adicionar(_mapper.Map<GrupoAlimento>(grupoAlimentoViewModel));

            if (!OperacaoValida()) return View(grupoAlimentoViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-grupo-alimento/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var grupoAlimentoViewModel = await ObterGrupoAlimento(id);

            if (grupoAlimentoViewModel == null) return NotFound();

            return View(grupoAlimentoViewModel);
        }

        [HttpPost]
        [Route("editar-grupo-alimento/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id, GrupoAlimentoViewModel grupoAlimentoViewModel)
        {
            if (id != grupoAlimentoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(grupoAlimentoViewModel);

            var grupoAlimentoDb = await ObterGrupoAlimento(id);

            if (grupoAlimentoDb == null) return NotFound();

            grupoAlimentoDb.Codigo = grupoAlimentoViewModel.Codigo;
            grupoAlimentoDb.Descricao = grupoAlimentoViewModel.Descricao;
            grupoAlimentoDb.Ativo = grupoAlimentoViewModel.Ativo;

            await _grupoAlimentoService.Atualizar(_mapper.Map<GrupoAlimento>(grupoAlimentoDb));

            if (!OperacaoValida()) return View(grupoAlimentoViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-grupo-alimento/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var grupoAlimentoViewModel = await ObterGrupoAlimento(id);

            if (grupoAlimentoViewModel == null) return NotFound();

            return View(grupoAlimentoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-grupo-alimento/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grupoAlimentoViewModel = await ObterGrupoAlimento(id);

            if (grupoAlimentoViewModel == null) return NotFound();

            await _grupoAlimentoService.Remover(id);

            if (!OperacaoValida()) return View(grupoAlimentoViewModel);

            return RedirectToAction("Index");
        }

        private async Task<GrupoAlimentoViewModel> ObterGrupoAlimento(Guid id)
        {
            return _mapper.Map<GrupoAlimentoViewModel>(await _grupoAlimentoRepository.ObterGrupoAlimento(id));
        }
    }
}