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
    public class UnidadesMedidasController : BaseController
    {
        private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;
        private readonly IUnidadeMedidaService _unidadeMedidaService;
        private readonly IMapper _mapper;

        public UnidadesMedidasController(IUnidadeMedidaRepository unidadeMedidaRepository,
            IUnidadeMedidaService unidadeMedidaService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _unidadeMedidaService = unidadeMedidaService;
            _mapper = mapper;
        }

        [Route("unidades-medidas")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<UnidadeMedidaViewModel>>(await _unidadeMedidaRepository.ObterTodos()));
        }

        [Route("unidade-medida/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var unidadeMedidaViewModel = await ObterUnidadeMedida(id);

            if (unidadeMedidaViewModel == null) return NotFound();

            return View(unidadeMedidaViewModel);
        }

        [Route("adicionar-unidade-medida")]
        public IActionResult Create()
        {
            return View(new UnidadeMedidaViewModel
            {
                Ativo = true
            });
        }

        [HttpPost]
        [Route("adicionar-unidade-medida")]
        public async Task<IActionResult> Create(UnidadeMedidaViewModel unidadeMedidaViewModel)
        {
            unidadeMedidaViewModel.Ativo = true;

            if (!ModelState.IsValid) return View(unidadeMedidaViewModel);

            await _unidadeMedidaService.Adicionar(_mapper.Map<UnidadeMedida>(unidadeMedidaViewModel));

            if (!OperacaoValida()) return View(unidadeMedidaViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-unidade-medida/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var unidadeMedidaViewModel = await ObterUnidadeMedida(id);

            if (unidadeMedidaViewModel == null) return NotFound();

            return View(unidadeMedidaViewModel);
        }

        [HttpPost]
        [Route("editar-unidade-medida/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id, UnidadeMedidaViewModel unidadeMedidaViewModel)
        {
            if (id != unidadeMedidaViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(unidadeMedidaViewModel);

            var unidadeMedidaDb = await ObterUnidadeMedida(id);

            if (unidadeMedidaDb == null) return NotFound();

            unidadeMedidaDb.Codigo = unidadeMedidaViewModel.Codigo;
            unidadeMedidaDb.Descricao = unidadeMedidaViewModel.Descricao;
            unidadeMedidaDb.Ativo = unidadeMedidaViewModel.Ativo;

            await _unidadeMedidaService.Atualizar(_mapper.Map<UnidadeMedida>(unidadeMedidaDb));

            if (!OperacaoValida()) return View(unidadeMedidaViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-unidade-medida/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var unidadeMedidaViewModel = await ObterUnidadeMedida(id);

            if (unidadeMedidaViewModel == null) return NotFound();

            return View(unidadeMedidaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-unidade-medida/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var unidadeMedidaViewModel = await ObterUnidadeMedida(id);

            if (unidadeMedidaViewModel == null) return NotFound();

            await _unidadeMedidaService.Remover(id);

            if (!OperacaoValida()) return View(unidadeMedidaViewModel);

            return RedirectToAction("Index");
        }

        private async Task<UnidadeMedidaViewModel> ObterUnidadeMedida(Guid id)
        {
            return _mapper.Map<UnidadeMedidaViewModel>(await _unidadeMedidaRepository.ObterUnidadeMedida(id));
        }
    }
}