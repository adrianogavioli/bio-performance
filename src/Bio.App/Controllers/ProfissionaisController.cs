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
    public class ProfissionaisController : BaseController
    {
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IProfissionalService _profissionalService;
        private readonly IMapper _mapper;

        public ProfissionaisController(IProfissionalRepository profissionalRepository,
            IProfissionalService profissionalService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _profissionalRepository = profissionalRepository;
            _profissionalService = profissionalService;
            _mapper = mapper;
        }

        [Route("profissionais")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProfissionalViewModel>>(await _profissionalRepository.ObterTodos()));
        }

        [Route("profissional/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var profissionalViewModel = await ObterProfissionalAtendimentos(id);

            if (profissionalViewModel == null) return NotFound();

            return View(profissionalViewModel);
        }

        [Route("adicionar-profissional")]
        public IActionResult Create()
        {
            return View(new ProfissionalViewModel
            {
                Ativo = true
            });
        }

        [HttpPost]
        [Route("adicionar-profissional")]
        public async Task<IActionResult> Create(ProfissionalViewModel profissionalViewModel)
        {
            profissionalViewModel.Ativo = true;
            
            if (!ModelState.IsValid) return View(profissionalViewModel);

            await _profissionalService.Adicionar(_mapper.Map<Profissional>(profissionalViewModel));

            if (!OperacaoValida()) return View(profissionalViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-profissional/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var profissionalViewModel = await ObterProfissionalAtendimentos(id);

            if (profissionalViewModel == null) return NotFound();

            return View(profissionalViewModel);
        }

        [HttpPost]
        [Route("editar-profissional/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ProfissionalViewModel profissionalViewModel)
        {
            if (id != profissionalViewModel.Id) return NotFound();

            var profissionalDb = await ObterProfissionalAtendimentos(id);

            if (profissionalDb == null) return NotFound();

            profissionalViewModel.Atendimentos = profissionalDb.Atendimentos;

            if (!ModelState.IsValid) return View(profissionalViewModel);

            profissionalDb.Nome = profissionalViewModel.Nome;
            profissionalDb.Documento = profissionalViewModel.Documento;
            profissionalDb.Especialidade = profissionalViewModel.Especialidade;
            profissionalDb.Ativo = profissionalViewModel.Ativo;

            await _profissionalService.Atualizar(_mapper.Map<Profissional>(profissionalDb));

            if (!OperacaoValida()) return View(profissionalViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-profissional/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var profissionalViewModel = await ObterProfissional(id);

            if (profissionalViewModel == null) return NotFound();

            return View(profissionalViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-profissional/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profissionalViewModel = await ObterProfissional(id);

            if (profissionalViewModel == null) return NotFound();

            await _profissionalService.Remover(id);

            if (!OperacaoValida()) return View(profissionalViewModel);

            return RedirectToAction("Index");
        }

        private async Task<ProfissionalViewModel> ObterProfissional(Guid id)
        {
            return _mapper.Map<ProfissionalViewModel>(await _profissionalRepository.ObterProfissional(id));
        }

        private async Task<ProfissionalViewModel> ObterProfissionalAtendimentos(Guid id)
        {
            return _mapper.Map<ProfissionalViewModel>(await _profissionalRepository.ObterProfissionalAtendimentos(id));
        }
    }
}
