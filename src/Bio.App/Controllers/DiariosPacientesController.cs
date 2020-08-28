using AutoMapper;
using Bio.App.ViewModels;
using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.App.Controllers
{
    public class DiariosPacientesController : BaseController
    {
        private readonly IDiarioPacienteRepository _diarioPacienteRepository;
        private readonly IDiarioPacienteService _diarioPacienteService;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ITreinoRepository _treinoRepository;
        private readonly IMapper _mapper;

        public DiariosPacientesController(
            IDiarioPacienteRepository diarioPacienteRepository,
            IDiarioPacienteService diarioPacienteService,
            IPacienteRepository pacienteRepository,
            ITreinoRepository treinoRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _diarioPacienteRepository = diarioPacienteRepository;
            _diarioPacienteService = diarioPacienteService;
            _pacienteRepository = pacienteRepository;
            _treinoRepository = treinoRepository;
            _mapper = mapper;
        }

        [Route("diarios-pacientes")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<DiarioPacienteViewModel>>(await _diarioPacienteRepository.ObterDiariosPacientes()));
        }

        [Route("diario-paciente/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var diarioPacienteViewModel = await ObterDiarioPaciente(id);

            if (diarioPacienteViewModel == null) return NotFound();

            return View(diarioPacienteViewModel);
        }

        [Route("adicionar-diario-paciente")]
        public async Task<IActionResult> Create()
        {
            var diarioPacienteViewModel = new DiarioPacienteViewModel();

            await PopularPacientesDropdown(diarioPacienteViewModel);

            return View(diarioPacienteViewModel);
        }

        [HttpPost]
        [Route("adicionar-diario-paciente")]
        public async Task<IActionResult> Create(DiarioPacienteViewModel diarioPacienteViewModel)
        {
            await PopularPacientesDropdown(diarioPacienteViewModel);

            if (!ModelState.IsValid) return View(diarioPacienteViewModel);

            await _diarioPacienteService.Adicionar(_mapper.Map<DiarioPaciente>(diarioPacienteViewModel));

            if (!OperacaoValida()) return View(diarioPacienteViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-diario-paciente/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var diarioPacienteViewModel = await ObterDiarioPaciente(id);

            if (diarioPacienteViewModel == null) return NotFound();

            return View(diarioPacienteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-diario-paciente/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var diarioPacienteViewModel = await ObterDiarioPaciente(id);

            if (diarioPacienteViewModel == null) return NotFound();

            await _diarioPacienteService.Remover(id);

            if (!OperacaoValida()) return View(diarioPacienteViewModel);

            return RedirectToAction("Index");
        }

        private async Task<DiarioPacienteViewModel> ObterDiarioPaciente(Guid id)
        {
            return _mapper.Map<DiarioPacienteViewModel>(await _diarioPacienteRepository.ObterDiarioPaciente(id));
        }

        private async Task<DiarioPacienteViewModel> PopularPacientesDropdown(DiarioPacienteViewModel diarioPacienteViewModel)
        {
            var pacientesViewModel = _mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteRepository.ObterTodos());

            diarioPacienteViewModel.PacientesDropdown = pacientesViewModel.OrderBy(p => p.Nome).ToList();

            return diarioPacienteViewModel;
        }

        [Route("diarios-pacientes-treinos-dropdown")]
        public async Task<JsonResult> PopularTreinosDropdown(Guid pacienteId)
        {
            var treinosViewModel = _mapper.Map<IEnumerable<Treino>>(await _treinoRepository.ObterTreinosAtivosPorPaciente(pacienteId));

            var jsonResult = new SelectList(treinosViewModel, "Id", "Titulo");

            return Json(jsonResult);
        }
    }
}
