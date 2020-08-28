using AutoMapper;
using Bio.App.ViewModels;
using Bio.Business.Interfaces;
using Bio.Business.Interfaces.Repositories;
using Bio.Business.Interfaces.Services;
using Bio.Business.Models;
using Bio.App.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.App.Controllers
{
    public class DietasController : BaseController
    {
        private readonly IDietaRepository _dietaRepository;
        private readonly IDietaService _dietaService;
        private readonly IDietaRefeicaoAlimentoRepository _dietaRefeicaoAlimentoRepository;
        private readonly IDietaRefeicaoAlimentoService _dietaRefeicaoAlimentoService;
        private readonly IDietaRefeicaoSubstituicaoRepository _dietaRefeicaoSubstituicaoRepository;
        private readonly IDietaRefeicaoSubstituicaoService _dietaRefeicaoSubstituicaoService;
        private readonly IAlimentoSubstituicaoRepository _alimentoSubstituicaoRepository;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IRefeicaoRepository _refeicaoRepository;
        private readonly IAlimentoRepository _alimentoRepository;
        private readonly IMapper _mapper;

        public DietasController(IDietaRepository dietaRepository,
            IDietaService dietaService,
            IDietaRefeicaoAlimentoRepository dietaRefeicaoAlimentoRepository,
            IDietaRefeicaoAlimentoService dietaRefeicaoAlimentoService,
            IDietaRefeicaoSubstituicaoRepository dietaRefeicaoSubstituicaoRepository,
            IDietaRefeicaoSubstituicaoService dietaRefeicaoSubstituicaoService,
            IAlimentoSubstituicaoRepository alimentoSubstituicaoRepository,
            IAtendimentoRepository atendimentoRepository,
            IRefeicaoRepository refeicaoRepository,
            IAlimentoRepository alimentoRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _dietaRepository = dietaRepository;
            _dietaService = dietaService;
            _dietaRefeicaoAlimentoRepository = dietaRefeicaoAlimentoRepository;
            _dietaRefeicaoAlimentoService = dietaRefeicaoAlimentoService;
            _dietaRefeicaoSubstituicaoRepository = dietaRefeicaoSubstituicaoRepository;
            _dietaRefeicaoSubstituicaoService = dietaRefeicaoSubstituicaoService;
            _alimentoSubstituicaoRepository = alimentoSubstituicaoRepository;
            _atendimentoRepository = atendimentoRepository;
            _refeicaoRepository = refeicaoRepository;
            _alimentoRepository = alimentoRepository;
            _mapper = mapper;
        }

        [Route("dietas")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<DietaViewModel>>(await _dietaRepository.ObterDietas()));
        }

        [Route("dieta/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var dietaViewModel = await ObterDietaRefeicoes(id);

            if (dietaViewModel == null) return NotFound();

            return View(dietaViewModel);
        }

        [Route("adicionar-dieta/{atendimentoId:guid}")]
        public async Task<IActionResult> Create(Guid atendimentoId)
        {
            var atendimentoViewModel = await ObterAtendimento(atendimentoId);

            if (atendimentoViewModel == null) NotFound();

            var dietaViewModel = new DietaViewModel
            {
                AtendimentoId = atendimentoId,
                Atendimento = atendimentoViewModel
            };

            return View(dietaViewModel);
        }

        [HttpPost]
        [Route("adicionar-dieta/{atendimentoId:guid}")]
        public async Task<IActionResult> Create(Guid atendimentoId, DietaViewModel dietaViewModel)
        {
            if (atendimentoId != dietaViewModel.AtendimentoId) return NotFound();

            dietaViewModel.Atendimento = await ObterAtendimento(atendimentoId);

            if (!ModelState.IsValid) return View(dietaViewModel);

            var dieta = _mapper.Map<Dieta>(dietaViewModel);

            await _dietaService.Adicionar(dieta);

            if (!OperacaoValida()) return View(dietaViewModel);

            return RedirectToAction("Edit", new { id = dieta.Id });
        }

        [Route("editar-dieta/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var dietaViewModel = await ObterDietaRefeicoes(id);

            if (dietaViewModel == null) return NotFound();

            return View(dietaViewModel);
        }

        [HttpPost]
        [Route("editar-dieta/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, DietaViewModel dietaViewModel)
        {
            if (id != dietaViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(dietaViewModel);

            var dietaDb = await _dietaRepository.ObterPorId(id);

            if (dietaDb == null) return NotFound();

            dietaDb.TaxaMetabolicaBasal = Convert.ToUInt32(dietaViewModel.TaxaMetabolicaBasal);
            dietaDb.Observacao = dietaViewModel.Observacao;
            dietaDb.Ativo = dietaViewModel.Ativo;

            await _dietaService.Atualizar(dietaDb);

            if (!OperacaoValida()) return View(dietaViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-dieta/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dietaViewModel = await ObterDietaRefeicoes(id);

            if (dietaViewModel == null) return NotFound();

            return View(dietaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-dieta/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dietaViewModel = await ObterDietaRefeicoes(id);

            if (dietaViewModel == null) return NotFound();

            await _dietaService.Remover(id);

            if (!OperacaoValida()) return View(dietaViewModel);

            return RedirectToAction("Index");
        }

        [Route("imprimir-dieta/{id:guid}")]
        public async Task<IActionResult> Print(Guid id)
        {
            var dietaViewModel = await ObterDietaRefeicoesSubstituicoes(id);

            if (dietaViewModel == null) return NotFound();

            return View(dietaViewModel);
        }

        [Route("detalhar-refeicao-dieta/{id:guid}")]
        public async Task<IActionResult> DetalharRefeicao(Guid id)
        {
            return PartialView("_DetalharRefeicao", await ObterDietaRefeicaoAlimentoSubstituicoes(id));
        }

        [Route("adicionar-refeicao-dieta/{id:guid}")]
        public async Task<IActionResult> AdicionarRefeicao(Guid id)
        {
            var dietaRefeicaoAlimentoViewModel = new DietaRefeicaoAlimentoViewModel
            {
                DietaId = id
            };

            await PopularRefeicoesDropdown(dietaRefeicaoAlimentoViewModel);

            return PartialView("_AdicionarRefeicao", dietaRefeicaoAlimentoViewModel);
        }

        [HttpPost]
        [Route("adicionar-refeicao-dieta/{id:guid}")]
        public async Task<IActionResult> AdicionarRefeicao(Guid id, DietaRefeicaoAlimentoViewModel dietaRefeicaoAlimentoViewModel)
        {
            if (id != dietaRefeicaoAlimentoViewModel.DietaId) return NotFound();

            await PopularRefeicoesDropdown(dietaRefeicaoAlimentoViewModel);

            ModelState.Remove("AlimentoId");
            ModelState.Remove("Alimento.Porcao");
            ModelState.Remove("Alimento.Carboidratos");
            ModelState.Remove("Alimento.Proteinas");
            ModelState.Remove("Alimento.Gorduras");

            if (!ModelState.IsValid) return PartialView("_AdicionarRefeicao", dietaRefeicaoAlimentoViewModel);

            var alimento = _alimentoRepository.Buscar(a => a.Descricao == dietaRefeicaoAlimentoViewModel.Alimento.Descricao).Result.FirstOrDefault();

            if (alimento == null)
            {
                ModelState.AddModelError(string.Empty, "O alimento informado não possui cadastro.");
                return PartialView("_AdicionarRefeicao", dietaRefeicaoAlimentoViewModel);
            }

            dietaRefeicaoAlimentoViewModel.AlimentoId = alimento.Id;
            dietaRefeicaoAlimentoViewModel.Id = Guid.NewGuid();

            await _dietaRefeicaoAlimentoService.Adicionar(_mapper.Map<DietaRefeicaoAlimento>(dietaRefeicaoAlimentoViewModel));

            if (!OperacaoValida()) return PartialView("_AdicionarRefeicao", dietaRefeicaoAlimentoViewModel);

            var url = Url.Action("ObterRefeicoes", "Dietas", new { id });
            return Json(new { success = true, url });
        }

        [Route("editar-refeicao-dieta/{id:guid}")]
        public async Task<IActionResult> EditarRefeicao(Guid id)
        {
            return PartialView("_EditarRefeicao", await ObterDietaRefeicaoAlimento(id));
        }

        [HttpPost]
        [Route("editar-refeicao-dieta/{id:guid}")]
        public async Task<IActionResult> EditarRefeicao(Guid id, DietaRefeicaoAlimentoViewModel dietaRefeicaoAlimentoViewModel)
        {
            if (id != dietaRefeicaoAlimentoViewModel.Id) return NotFound();

            ModelState.Remove("AlimentoId");
            ModelState.Remove("RefeicaoId");
            ModelState.Remove("Refeicao.Ordem");
            ModelState.Remove("Alimento.Porcao");
            ModelState.Remove("Alimento.Carboidratos");
            ModelState.Remove("Alimento.Proteinas");
            ModelState.Remove("Alimento.Gorduras");

            if (!ModelState.IsValid) return PartialView("_EditarRefeicao", dietaRefeicaoAlimentoViewModel);

            var dietaRefeicaoAlimentoDb = await ObterDietaRefeicaoAlimento(id);

            if (dietaRefeicaoAlimentoDb == null)
            {
                ModelState.AddModelError(string.Empty, "A refeição selecionada pode ter sido removida, favor verificar.");
                return PartialView("_EditarRefeicao", dietaRefeicaoAlimentoViewModel);
            }

            dietaRefeicaoAlimentoDb.Quantidade = dietaRefeicaoAlimentoViewModel.Quantidade;
            dietaRefeicaoAlimentoDb.Observacao = dietaRefeicaoAlimentoViewModel.Observacao;

            await _dietaRefeicaoAlimentoService.Atualizar(_mapper.Map<DietaRefeicaoAlimento>(dietaRefeicaoAlimentoDb));

            if (!OperacaoValida()) return PartialView("_EditarRefeicao", dietaRefeicaoAlimentoViewModel);

            var url = Url.Action("ObterRefeicoes", "Dietas", new { id = dietaRefeicaoAlimentoViewModel.DietaId });
            return Json(new { success = true, url });
        }

        [Route("remover-refeicao-dieta/{id:guid}")]
        public async Task<IActionResult> RemoverRefeicao(Guid id)
        {
            return PartialView("_RemoverRefeicao", await ObterDietaRefeicaoAlimento(id));
        }

        [HttpPost, ActionName("RemoverRefeicao")]
        [Route("remover-refeicao-dieta/{id:guid}")]
        public async Task<IActionResult> RemoverRefeicaoConfirmed(Guid id)
        {
            var dietaRefeicaoAlimentoViewModel = await ObterDietaRefeicaoAlimento(id);

            if (dietaRefeicaoAlimentoViewModel == null) return NotFound();

            await _dietaRefeicaoAlimentoService.Remover(id);

            if (!OperacaoValida()) return PartialView("_RemoverRefeicao", dietaRefeicaoAlimentoViewModel);

            var url = Url.Action("ObterRefeicoes", "Dietas", new { id = dietaRefeicaoAlimentoViewModel.DietaId });
            return Json(new { success = true, url });
        }

        [Route("gerenciar-substituicao-dieta/{dietaRefeicaoAlimentoId:guid}")]
        public async Task<IActionResult> GerenciarSubstituicao(Guid dietaRefeicaoAlimentoId)
        {
            var dietaRefeicaoAlimentoViewModel = await ObterDietaRefeicaoAlimentoSubstituicoes(dietaRefeicaoAlimentoId);

            if (dietaRefeicaoAlimentoViewModel == null) return NotFound();

            return PartialView("_GerenciarSubstituicoes", dietaRefeicaoAlimentoViewModel);
        }

        [Route("adicionar-substituicao-dieta/{dietaRefeicaoAlimentoId:guid}")]
        public async Task<IActionResult> AdicionarSubstituicao(Guid dietaRefeicaoAlimentoId)
        {
            var dietaRefeicaoAlimentoViewModel = await ObterDietaRefeicaoAlimentoSubstituicoes(dietaRefeicaoAlimentoId);

            if (dietaRefeicaoAlimentoViewModel == null) return NotFound();

            var dietaRefeicaoSubstituicaoViewModel = new DietaRefeicaoSubstituicaoViewModel
            {
                DietaRefeicaoAlimento = dietaRefeicaoAlimentoViewModel,
                DietaRefeicaoAlimentoId = dietaRefeicaoAlimentoId
            };

            await PopularAlimentosSubstitutosDropdown(dietaRefeicaoSubstituicaoViewModel);

            return PartialView("_AdicionarSubstituicao", dietaRefeicaoSubstituicaoViewModel);
        }

        [HttpPost]
        [Route("adicionar-substituicao-dieta/{dietaRefeicaoAlimentoId:guid}")]
        public async Task<IActionResult> AdicionarSubstituicao(Guid dietaRefeicaoAlimentoId, DietaRefeicaoSubstituicaoViewModel dietaRefeicaoSubstituicaoViewModel)
        {
            if (dietaRefeicaoAlimentoId != dietaRefeicaoSubstituicaoViewModel.DietaRefeicaoAlimentoId) return NotFound();

            await PopularAlimentosSubstitutosDropdown(dietaRefeicaoSubstituicaoViewModel);

            dietaRefeicaoSubstituicaoViewModel.DietaRefeicaoAlimento = await ObterDietaRefeicaoAlimentoSubstituicoes(dietaRefeicaoAlimentoId);

            ModelState.Remove("Quantidade");
            ModelState.Remove("DietaRefeicaoAlimento.Quantidade");
            ModelState.Remove("DietaRefeicaoAlimento.RefeicaoId");

            if (!ModelState.IsValid) return PartialView("_AdicionarSubstituicao", dietaRefeicaoSubstituicaoViewModel);

            await ObterAlimentoSubstituicao(dietaRefeicaoSubstituicaoViewModel);

            DietaCalculos.CalcularQuantidadeSubstituicao(dietaRefeicaoSubstituicaoViewModel);
            dietaRefeicaoSubstituicaoViewModel.Id = Guid.NewGuid();

            await _dietaRefeicaoSubstituicaoService.Adicionar(_mapper.Map<DietaRefeicaoSubstituicao>(dietaRefeicaoSubstituicaoViewModel));

            if (!OperacaoValida()) return PartialView("_AdicionarSubstituicao", dietaRefeicaoSubstituicaoViewModel);

            return PartialView("_GerenciarSubstituicoes", await ObterDietaRefeicaoAlimentoSubstituicoes(dietaRefeicaoAlimentoId));
        }

        [Route("remover-substituicao-dieta/{id:guid}")]
        public async Task<IActionResult> RemoverSubstituicao(Guid id)
        {
            return PartialView("_RemoverSubstituicao", await ObterDietaRefeicaoSubstituicao(id));
        }

        [HttpPost, ActionName("RemoverSubstituicao")]
        [Route("remover-substituicao-dieta/{id:guid}")]
        public async Task<IActionResult> RemoverSubstituicaoConfirmed(Guid id)
        {
            var dietaRefeicaoSubstituicaoViewModel = await ObterDietaRefeicaoSubstituicao(id);

            if (dietaRefeicaoSubstituicaoViewModel == null) return NotFound();

            await _dietaRefeicaoSubstituicaoService.Remover(id);

            if (!OperacaoValida()) return PartialView("_RemoverSubstituicao", dietaRefeicaoSubstituicaoViewModel);

            return PartialView("_GerenciarSubstituicoes", await ObterDietaRefeicaoAlimentoSubstituicoes(dietaRefeicaoSubstituicaoViewModel.DietaRefeicaoAlimentoId));
        }

        public async Task<IActionResult> ObterRefeicoes(Guid id)
        {
            var dietaViewModel = await ObterDietaRefeicoes(id);

            if (dietaViewModel == null) return NotFound();

            return PartialView("_Refeicoes", dietaViewModel);
        }

        private async Task<DietaViewModel> ObterDietaRefeicoes(Guid id)
        {
            var dietaViewModel = _mapper.Map<DietaViewModel>(await _dietaRepository.ObterDietaRefeicoesAlimentos(id));

            dietaViewModel.Refeicoes.ForEach(r => DietaCalculos.CalcularValoresNutricionais(r));

            if (dietaViewModel.Refeicoes.Count > 0)
            {
                DietaCalculos.CalcularValoresNutricionaisTotais(dietaViewModel);

                dietaViewModel.Refeicoes = dietaViewModel.Refeicoes.OrderBy(r => r.Refeicao.Ordem).ThenBy(r => r.Alimento.Descricao).ToList();
            }

            DietaCalculos.CalcularDiferencaCalorica(dietaViewModel);

            return dietaViewModel;
        }

        private async Task<DietaViewModel> ObterDietaRefeicoesSubstituicoes(Guid id)
        {
            var dietaViewModel = _mapper.Map<DietaViewModel>(await _dietaRepository.ObterDietaRefeicoesSubstituicoes(id));

            dietaViewModel.Refeicoes.ForEach(r => DietaCalculos.CalcularValoresNutricionais(r));

            if (dietaViewModel.Refeicoes.Count > 0)
            {
                DietaCalculos.CalcularValoresNutricionaisTotais(dietaViewModel);

                dietaViewModel.Refeicoes = dietaViewModel.Refeicoes.OrderBy(r => r.Refeicao.Ordem).ThenBy(r => r.Alimento.Descricao).ToList();
            }

            DietaCalculos.CalcularDiferencaCalorica(dietaViewModel);

            return dietaViewModel;
        }

        private async Task<DietaRefeicaoSubstituicaoViewModel> ObterDietaRefeicaoSubstituicao(Guid id)
        {
            var dietaRefeicaoSubstituicaoViewModel = _mapper.Map<DietaRefeicaoSubstituicaoViewModel>(await _dietaRefeicaoSubstituicaoRepository.ObterDietaRefeicaoSubstituicao(id));

            if (dietaRefeicaoSubstituicaoViewModel.DietaRefeicaoAlimento != null) DietaCalculos.CalcularValoresNutricionais(dietaRefeicaoSubstituicaoViewModel.DietaRefeicaoAlimento);

            if (dietaRefeicaoSubstituicaoViewModel != null) DietaCalculos.CalcularValoresNutricionaisSubstituicao(dietaRefeicaoSubstituicaoViewModel);

            return dietaRefeicaoSubstituicaoViewModel;
        }

        private async Task<DietaRefeicaoAlimentoViewModel> ObterDietaRefeicaoAlimento(Guid id)
        {
            var dietaRefeicaoAlimentoViewModel = _mapper.Map<DietaRefeicaoAlimentoViewModel>(await _dietaRefeicaoAlimentoRepository.ObterDietaRefeicaoAlimento(id));

            if (dietaRefeicaoAlimentoViewModel != null) DietaCalculos.CalcularValoresNutricionais(dietaRefeicaoAlimentoViewModel);

            return dietaRefeicaoAlimentoViewModel;
        }

        private async Task<DietaRefeicaoAlimentoViewModel> ObterDietaRefeicaoAlimentoSubstituicoes(Guid id)
        {
            var dietaRefeicaoAlimentoViewModel = _mapper.Map<DietaRefeicaoAlimentoViewModel>(await _dietaRefeicaoAlimentoRepository.ObterDietaRefeicaoAlimentoSubstituicoes(id));

            if (dietaRefeicaoAlimentoViewModel != null) DietaCalculos.CalcularValoresNutricionais(dietaRefeicaoAlimentoViewModel);

            dietaRefeicaoAlimentoViewModel.DietasRefeicoesSubstituicoes.ForEach(s => DietaCalculos.CalcularValoresNutricionaisSubstituicao(s));

            return dietaRefeicaoAlimentoViewModel;
        }

        private async Task<DietaRefeicaoSubstituicaoViewModel> ObterAlimentoSubstituicao(DietaRefeicaoSubstituicaoViewModel dietaRefeicaoSubstituicaoViewModel)
        {
            dietaRefeicaoSubstituicaoViewModel.Alimento = _mapper.Map<AlimentoViewModel>(await _alimentoRepository.ObterAlimento(dietaRefeicaoSubstituicaoViewModel.AlimentoId));

            return dietaRefeicaoSubstituicaoViewModel;
        }

        private async Task<AtendimentoViewModel> ObterAtendimento(Guid atendimentoId)
        {
            return _mapper.Map<AtendimentoViewModel>(await _atendimentoRepository.ObterAtendimento(atendimentoId));
        }

        private async Task<DietaRefeicaoAlimentoViewModel> PopularRefeicoesDropdown(DietaRefeicaoAlimentoViewModel dietaRefeicaoAlimentoViewModel)
        {
            var refeicoes = await _refeicaoRepository.Buscar(r => r.Ativo);

            dietaRefeicaoAlimentoViewModel.RefeicoesDropdown = _mapper.Map<List<RefeicaoViewModel>>(refeicoes.OrderBy(r => r.Ordem));

            return dietaRefeicaoAlimentoViewModel;
        }

        private async Task<DietaRefeicaoSubstituicaoViewModel> PopularAlimentosSubstitutosDropdown(DietaRefeicaoSubstituicaoViewModel dietaRefeicaoSubstituicaoViewModel)
        {
            var alimentosSubstitutos = await _alimentoSubstituicaoRepository.ObterAlimentosSubstituicoesPorAlimentoId(dietaRefeicaoSubstituicaoViewModel.DietaRefeicaoAlimento.AlimentoId);

            dietaRefeicaoSubstituicaoViewModel.AlimentosSubstitutosDropdown = _mapper.Map<List<AlimentoSubstituicaoViewModel>>(alimentosSubstitutos.OrderBy(a => a.AlimentoSubstituto.Descricao));

            return dietaRefeicaoSubstituicaoViewModel;
        }
    }
}
