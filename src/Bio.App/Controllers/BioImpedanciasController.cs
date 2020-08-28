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
    public class BioImpedanciasController : BaseController
    {
        private readonly IBioImpedanciaRepository _bioImpedanciaRepository;
        private readonly IBioImpedanciaService _bioImpedanciaService;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;

        public BioImpedanciasController(IBioImpedanciaRepository bioImpedanciaRepository,
            IBioImpedanciaService bioImpedanciaService,
            IPacienteRepository pacienteRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _bioImpedanciaRepository = bioImpedanciaRepository;
            _bioImpedanciaService = bioImpedanciaService;
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        [Route("Bioimpedancias")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<BioImpedanciaViewModel>>(await _bioImpedanciaRepository.ObterBioImpedancias()));
        }

        [Route("Bioimpedancia/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var bioImpedanciaViewModel = await ObterBioimpedancia(id);

            if (bioImpedanciaViewModel == null) return NotFound();

            return View(bioImpedanciaViewModel);
        }

        [Route("adicionar-bioimpedancia")]
        public async Task<IActionResult> Create()
        {
            var bioimpedanciaViewModel = new BioImpedanciaViewModel
            {
                PacientesDropdown = await ObterPacientesDropdown()
            };

            return View(bioimpedanciaViewModel);
        }

        [HttpPost]
        [Route("adicionar-bioimpedancia")]
        public async Task<IActionResult> Create(BioImpedanciaViewModel bioImpedanciaViewModel)
        {
            bioImpedanciaViewModel.PacientesDropdown = await ObterPacientesDropdown();

            if (!ModelState.IsValid) return View(bioImpedanciaViewModel);

            await _bioImpedanciaService.Adicionar(_mapper.Map<BioImpedancia>(bioImpedanciaViewModel));

            if (!OperacaoValida()) return View(bioImpedanciaViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-bioimpedancia/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var bioImpedanciaViewModel = await ObterBioimpedancia(id);
            
            if (bioImpedanciaViewModel == null) return NotFound();

            bioImpedanciaViewModel.PacientesDropdown = await ObterPacientesDropdown();

            return View(bioImpedanciaViewModel);
        }

        [HttpPost]
        [Route("editar-bioimpedancia/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, BioImpedanciaViewModel bioImpedanciaViewModel)
        {
            if (id != bioImpedanciaViewModel.Id) return NotFound();

            bioImpedanciaViewModel.PacientesDropdown = await ObterPacientesDropdown();

            if (!ModelState.IsValid) return View(bioImpedanciaViewModel);

            var bioimpedanciaDb = await ObterBioimpedancia(id);

            if (bioimpedanciaDb == null) return NotFound();

            bioimpedanciaDb.AguaCorporal = bioImpedanciaViewModel.AguaCorporal;
            bioimpedanciaDb.GorduraCorporal = bioImpedanciaViewModel.GorduraCorporal;
            bioimpedanciaDb.Proteinas = bioImpedanciaViewModel.Proteinas;
            bioimpedanciaDb.Minerais = bioImpedanciaViewModel.Minerais;
            bioimpedanciaDb.TaxaMetabolicaBasal = bioImpedanciaViewModel.TaxaMetabolicaBasal;
            bioimpedanciaDb.PacienteId = bioImpedanciaViewModel.PacienteId;

            await _bioImpedanciaService.Atualizar(_mapper.Map<BioImpedancia>(bioimpedanciaDb));

            if (!OperacaoValida()) return View(bioImpedanciaViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-bioimpedancia/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bioImpedanciaViewModel = await ObterBioimpedancia(id);

            if (bioImpedanciaViewModel == null) return NotFound();

            return View(bioImpedanciaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-bioimpedancia/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bioImpedanciaViewModel = await ObterBioimpedancia(id);

            if (bioImpedanciaViewModel == null) return NotFound();

            await _bioImpedanciaService.Remover(id);

            if (!OperacaoValida()) return View(bioImpedanciaViewModel);

            return RedirectToAction("Index");
        }

        private async Task<BioImpedanciaViewModel> ObterBioimpedancia(Guid id)
        {
            return _mapper.Map<BioImpedanciaViewModel>(await _bioImpedanciaRepository.ObterBioImpedancia(id));
        }

        private async Task<List<PacienteViewModel>> ObterPacientesDropdown()
        {
            var pacientes = await _pacienteRepository.ObterTodos();

            return _mapper.Map<List<PacienteViewModel>>(pacientes.OrderBy(p => p.Nome));
        }
    }
}
