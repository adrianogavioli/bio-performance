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
    public class RefeicoesController : BaseController
    {
        private readonly IRefeicaoRepository _refeicaoRepository;
        private readonly IRefeicaoService _refeicaoService;
        private readonly IMapper _mapper;

        public RefeicoesController(IRefeicaoRepository refeicaoRepository,
            IRefeicaoService refeicaoService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _refeicaoRepository = refeicaoRepository;
            _refeicaoService = refeicaoService;
            _mapper = mapper;
        }

        [Route("refeicoes")]
        public async Task<IActionResult> Index()
        {
            var refeicoes = await _refeicaoRepository.ObterTodos();

            return View(_mapper.Map<IEnumerable<RefeicaoViewModel>>(refeicoes.OrderBy(r => r.Ordem)));
        }

        [Route("refeicao/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var refeicaoViewModel = await ObterRefeicao(id);

            if (refeicaoViewModel == null) return NotFound();

            return View(refeicaoViewModel);
        }

        [Route("adicionar-refeicao")]
        public IActionResult Create()
        {
            return View(new RefeicaoViewModel
            {
                Ativo = true
            });
        }

        [HttpPost]
        [Route("adicionar-refeicao")]
        public async Task<IActionResult> Create(RefeicaoViewModel refeicaoViewModel)
        {
            refeicaoViewModel.Ativo = true;

            if (!ModelState.IsValid) return View(refeicaoViewModel);

            await _refeicaoService.Adicionar(_mapper.Map<Refeicao>(refeicaoViewModel));

            if (!OperacaoValida()) return View(refeicaoViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-refeicao/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var refeicaoViewModel = await ObterRefeicao(id);

            if (refeicaoViewModel == null) return NotFound();

            return View(refeicaoViewModel);
        }

        [HttpPost]
        [Route("editar-refeicao/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id, RefeicaoViewModel refeicaoViewModel)
        {
            if (id != refeicaoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(refeicaoViewModel);

            var refeicaoDb = await ObterRefeicao(id);

            if (refeicaoDb == null) return NotFound();

            refeicaoDb.Ordem = refeicaoViewModel.Ordem;
            refeicaoDb.Descricao = refeicaoViewModel.Descricao;
            refeicaoDb.Ativo = refeicaoViewModel.Ativo;

            await _refeicaoService.Atualizar(_mapper.Map<Refeicao>(refeicaoDb));

            if (!OperacaoValida()) return View(refeicaoViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-refeicao/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var refeicaoViewModel = await ObterRefeicao(id);

            if (refeicaoViewModel == null) return NotFound();
            
            return View(refeicaoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-refeicao/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var refeicaoViewModel = await ObterRefeicao(id);

            if (refeicaoViewModel == null) return NotFound();

            await _refeicaoService.Remover(id);

            if (!OperacaoValida()) return View(refeicaoViewModel);
            
            return RedirectToAction("Index");
        }

        private async Task<RefeicaoViewModel> ObterRefeicao(Guid id)
        {
            return _mapper.Map<RefeicaoViewModel>(await _refeicaoRepository.ObterRefeicao(id));
        }
    }
}
