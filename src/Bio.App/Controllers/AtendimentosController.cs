using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bio.App.ViewModels;
using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bio.App.Controllers
{
    public class AtendimentosController : BaseController
    {
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IAtendimentoService _atendimentoService;
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;

        public AtendimentosController(
            IAtendimentoRepository atendimentoRepository,
            IAtendimentoService atendimentoService,
            IProfissionalRepository profissionalRepository,
            IPacienteRepository pacienteRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _atendimentoRepository = atendimentoRepository;
            _atendimentoService = atendimentoService;
            _profissionalRepository = profissionalRepository;
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        [Route("Atendimentos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<AtendimentoViewModel>>(await _atendimentoRepository.ObterAtendimentos()));
        }

        [Route("Atendimento/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var atendimentoViewModel = await ObterAtendimentoPlanosTreinosDietas(id);

            if (atendimentoViewModel == null) return NotFound();

            return View(atendimentoViewModel);
        }

        [Route("adicionar-atendimento")]
        public async Task<IActionResult> Create()
        {
            var atendimentoViewModel = new AtendimentoViewModel();

            await PopularProfissionaisDropdown(atendimentoViewModel);
            await PopularPacientesDropdown(atendimentoViewModel);

            return View(atendimentoViewModel);
        }

        [HttpPost]
        [Route("adicionar-atendimento")]
        public async Task<IActionResult> Create(AtendimentoViewModel atendimentoViewModel)
        {
            await PopularProfissionaisDropdown(atendimentoViewModel);
            await PopularPacientesDropdown(atendimentoViewModel);

            if (!ModelState.IsValid) return View(atendimentoViewModel);

            await _atendimentoService.Adicionar(_mapper.Map<Atendimento>(atendimentoViewModel));

            if (!OperacaoValida()) return View(atendimentoViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-atendimento/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var atendimentoViewModel = await ObterAtendimentoPlanosTreinosDietas(id);

            if (atendimentoViewModel == null) return NotFound();

            await PopularProfissionaisDropdown(atendimentoViewModel);
            await PopularPacientesDropdown(atendimentoViewModel);

            return View(atendimentoViewModel);
        }

        [HttpPost]
        [Route("editar-atendimento/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id, AtendimentoViewModel atendimentoViewModel)
        {
            if (id != atendimentoViewModel.Id) return NotFound();

            var atendimentoDb = await ObterAtendimentoPlanosTreinosDietas(id);

            if (atendimentoDb == null) return NotFound();

            atendimentoViewModel.Paciente = atendimentoDb.Paciente;
            atendimentoViewModel.Profissional = atendimentoDb.Profissional;
            atendimentoViewModel.Dietas = atendimentoDb.Dietas;
            atendimentoViewModel.PlanosTreinos = atendimentoDb.PlanosTreinos;

            if (!ModelState.IsValid) return View(atendimentoViewModel);

            atendimentoDb.Objetivo = atendimentoViewModel.Objetivo;
            atendimentoDb.RestricaoMedica = atendimentoViewModel.RestricaoMedica;
            atendimentoDb.RestricaoAlimentar = atendimentoViewModel.RestricaoAlimentar;
            atendimentoDb.Observacao = atendimentoViewModel.Observacao;

            await _atendimentoService.Atualizar(_mapper.Map<Atendimento>(atendimentoDb));

            if (!OperacaoValida()) return View(atendimentoViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-atendimento/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var atendimentoViewModel = await ObterAtendimento(id);

            if (atendimentoViewModel == null) return NotFound();

            return View(atendimentoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-atendimento/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var atendimentoViewModel = await ObterAtendimento(id);

            if (atendimentoViewModel == null) return NotFound();

            await _atendimentoService.Remover(id);

            if (!OperacaoValida()) return View(atendimentoViewModel);

            return RedirectToAction("Index");
        }

        private async Task<AtendimentoViewModel> ObterAtendimento(Guid id)
        {
            return _mapper.Map<AtendimentoViewModel>(await _atendimentoRepository.ObterAtendimento(id));
        }

        private async Task<AtendimentoViewModel> ObterAtendimentoPlanosTreinosDietas(Guid id)
        {
            return _mapper.Map<AtendimentoViewModel>(await _atendimentoRepository.ObterAtendimentoPlanosTreinosDietas(id));
        }

        private async Task<AtendimentoViewModel> PopularProfissionaisDropdown(AtendimentoViewModel atendimentoViewModel)
        {
            var profissionais = await _profissionalRepository.Buscar(p => p.Ativo);

            atendimentoViewModel.ProfissionaisDropdown = _mapper.Map<List<ProfissionalViewModel>>(profissionais.OrderBy(p => p.Nome));

            return atendimentoViewModel;
        }

        private async Task<AtendimentoViewModel> PopularPacientesDropdown(AtendimentoViewModel atendimentoViewModel)
        {
            var pacientes = await _pacienteRepository.ObterTodos();

            atendimentoViewModel.PacientesDropdown = _mapper.Map<List<PacienteViewModel>>(pacientes.OrderBy(p => p.Nome));

            return atendimentoViewModel;
        }
    }
}