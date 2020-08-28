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
using Bio.App.Utilities;

namespace Bio.App.Controllers
{
    public class PacientesController : BaseController
    {
        private readonly IPacienteService _pacienteService;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;

        public PacientesController(IPacienteService pacienteService,
            IPacienteRepository pacienteRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _pacienteService = pacienteService;
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        [Route("pacientes")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteRepository.ObterTodos()));
        }

        [Route("paciente/{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var pacienteViewModel = await ObterPacienteBioimpedanciasAtendimentosDiarios(id);

            if (pacienteViewModel == null) return NotFound();

            ViewBag.ArrayGraficoComposicaoCorporal = GerarArrayGraficoComposicaoCorporal(pacienteViewModel);
            ViewBag.ArrayGraficoPercentualGordura = GerarArrayGraficoPercentualGordura(pacienteViewModel);

            return View(pacienteViewModel);
        }

        [Route("adicionar-paciente")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("adicionar-paciente")]
        public async Task<IActionResult> Create(PacienteViewModel pacienteViewModel)
        {
            if (!ModelState.IsValid) return View(pacienteViewModel);

            await _pacienteService.Adicionar(_mapper.Map<Paciente>(pacienteViewModel));

            if (!OperacaoValida()) return View(pacienteViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-paciente/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var pacienteViewModel = await ObterPacienteBioimpedanciasAtendimentosDiarios(id);

            if (pacienteViewModel == null) return NotFound();

            ViewBag.ArrayGraficoComposicaoCorporal = GerarArrayGraficoComposicaoCorporal(pacienteViewModel);
            ViewBag.ArrayGraficoPercentualGordura = GerarArrayGraficoPercentualGordura(pacienteViewModel);

            return View(pacienteViewModel);
        }

        [HttpPost]
        [Route("editar-paciente/{id:Guid}")]
        public async Task<IActionResult> Edit(Guid id, PacienteViewModel pacienteViewModel)
        {
            if (id != pacienteViewModel.Id) return NotFound();

            var pacienteDb = await ObterPacienteBioimpedanciasAtendimentosDiarios(id);

            if (pacienteDb == null) return NotFound();

            pacienteViewModel.Atendimentos = pacienteDb.Atendimentos;
            pacienteViewModel.BioImpedancias = pacienteDb.BioImpedancias;
            pacienteViewModel.Diarios = pacienteDb.Diarios;

            ViewBag.ArrayGraficoComposicaoCorporal = GerarArrayGraficoComposicaoCorporal(pacienteViewModel);
            ViewBag.ArrayGraficoPercentualGordura = GerarArrayGraficoPercentualGordura(pacienteViewModel);

            if (!ModelState.IsValid) return View(pacienteViewModel);

            pacienteDb.Nome = pacienteViewModel.Nome;
            pacienteDb.CPF = pacienteViewModel.CPF;
            pacienteDb.DataNascimento = pacienteViewModel.DataNascimento;
            pacienteDb.Altura = pacienteViewModel.Altura;
            pacienteDb.Genero = pacienteViewModel.Genero;

            await _pacienteService.Atualizar(_mapper.Map<Paciente>(pacienteDb));

            if (!OperacaoValida()) return View(pacienteViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-paciente/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pacienteViewModel = await ObterPaciente(id);

            if (pacienteViewModel == null) return NotFound();

            return View(pacienteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-paciente/{id:Guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pacienteViewModel = await ObterPaciente(id);

            if (pacienteViewModel == null) return NotFound();

            await _pacienteService.Remover(id);

            if (!OperacaoValida()) return View(pacienteViewModel);

            return RedirectToAction("Index");
        }

        private async Task<PacienteViewModel> ObterPaciente(Guid id)
        {
            return _mapper.Map<PacienteViewModel>(await _pacienteRepository.ObterPaciente(id));
        }

        private async Task<PacienteViewModel> ObterPacienteBioimpedanciasAtendimentosDiarios(Guid id)
        {
            var pacienteViewModel = _mapper.Map<PacienteViewModel>(await _pacienteRepository.ObterPacienteBioImpedanciasAtendimentosDiarios(id));

            pacienteViewModel.BioImpedancias.ForEach(b => BioimpedanciaCalculos.CalcularDadosAdicionais(b));

            return pacienteViewModel;
        }

        private string GerarArrayGraficoComposicaoCorporal(PacienteViewModel pacienteViewModel)
        {
            var arrayGrafico = "['Data', 'Gordura Corporal', 'Massa Magra', 'Peso'],";

            foreach (var bioimpedancia in pacienteViewModel.BioImpedancias)
            {
                arrayGrafico += $"['{bioimpedancia.DataCadastro.ToShortDateString()}'," +
                    $"{GraficosUtilities.TratarDecimais(bioimpedancia.GorduraCorporal)}," +
                    $"{GraficosUtilities.TratarDecimais(bioimpedancia.MassaMagra)}," +
                    $"{GraficosUtilities.TratarDecimais(bioimpedancia.Peso)}],";
            }

            return arrayGrafico.Substring(0, arrayGrafico.Length - 1);
        }

        private string GerarArrayGraficoPercentualGordura(PacienteViewModel pacienteViewModel)
        {
            var arrayGrafico = "['Data', 'Gordura Corporal', 'Massa Magra', 'Peso', '% Gordura'],";

            foreach (var bioimpedancia in pacienteViewModel.BioImpedancias)
            {
                arrayGrafico += $"['{bioimpedancia.DataCadastro.ToShortDateString()}'," +
                    $"{GraficosUtilities.TratarDecimais(bioimpedancia.GorduraCorporal)}," +
                    $"{GraficosUtilities.TratarDecimais(bioimpedancia.MassaMagra)}," +
                    $"{GraficosUtilities.TratarDecimais(bioimpedancia.Peso)}," +
                    $"{GraficosUtilities.TratarDecimais(bioimpedancia.PercentGordura)}],";
            }

            return arrayGrafico.Substring(0, arrayGrafico.Length - 1);
        }
    }
}
