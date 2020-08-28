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
    public class AlimentosController : BaseController
    {
        private readonly IAlimentoRepository _alimentoRepository;
        private readonly IAlimentoService _alimentoService;
        private readonly IAlimentoSubstituicaoRepository _alimentoSubstituicaoRepository;
        private readonly IAlimentoSubstituicaoService _alimentoSubstituicaoService;
        private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;
        private readonly IGrupoAlimentoRepository _grupoAlimentoRepository;
        private readonly IMapper _mapper;

        public AlimentosController(IAlimentoRepository alimentoRepository,
            IAlimentoService alimentoService,
            IAlimentoSubstituicaoRepository alimentoSubstituicaoRepository,
            IAlimentoSubstituicaoService alimentoSubstituicaoService,
            IUnidadeMedidaRepository unidadeMedidaRepository,
            IGrupoAlimentoRepository grupoAlimentoRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _alimentoRepository = alimentoRepository;
            _alimentoService = alimentoService;
            _alimentoSubstituicaoRepository = alimentoSubstituicaoRepository;
            _alimentoSubstituicaoService = alimentoSubstituicaoService;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _grupoAlimentoRepository = grupoAlimentoRepository;
            _mapper = mapper;
        }

        [Route("alimentos")]
        public async Task<IActionResult> Index()
        {
            var alimentosViewModel = _mapper.Map<IEnumerable<AlimentoViewModel>>(await _alimentoRepository.ObterAlimentos());

            return View(alimentosViewModel.OrderBy(a => a.Descricao));
        }

        [Route("alimento/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var alimentoViewModel = await ObterAlimentoSubstituicoes(id);

            if (alimentoViewModel == null) return NotFound();

            return View(alimentoViewModel);
        }

        [Route("adicionar-alimento")]
        public async Task<IActionResult> Create()
        {
            var alimentoViewModel = new AlimentoViewModel
            {
                UnidadesMedidasDropdown = await PopularUnidadesMedidasDropdown(),
                GruposAlimentosDropdown = await PopularGruposAlimentosDropdown()
            };

            return View(alimentoViewModel);
        }

        [HttpPost]
        [Route("adicionar-alimento")]
        public async Task<IActionResult> Create(AlimentoViewModel alimentoViewModel)
        {
            alimentoViewModel.UnidadesMedidasDropdown = await PopularUnidadesMedidasDropdown();
            alimentoViewModel.GruposAlimentosDropdown = await PopularGruposAlimentosDropdown();

            if (!ModelState.IsValid) return View(alimentoViewModel);

            alimentoViewModel.Calorias = CalcularCalorias(alimentoViewModel);

            await _alimentoService.Adicionar(_mapper.Map<Alimento>(alimentoViewModel));

            if (!OperacaoValida()) return View(alimentoViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-alimento/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var alimentoViewModel = await ObterAlimentoSubstituicoes(id);

            if (alimentoViewModel == null) return NotFound();

            alimentoViewModel.UnidadesMedidasDropdown = await PopularUnidadesMedidasDropdown();
            alimentoViewModel.GruposAlimentosDropdown = await PopularGruposAlimentosDropdown();

            return View(alimentoViewModel);
        }

        [HttpPost]
        [Route("editar-alimento/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, AlimentoViewModel alimentoViewModel)
        {
            if (id != alimentoViewModel.Id) return NotFound();

            var alimentoDb = await ObterAlimentoSubstituicoes(id);

            if (alimentoDb == null) return NotFound();

            alimentoViewModel.AlimentosSubstituicoes = alimentoDb.AlimentosSubstituicoes;
            alimentoViewModel.UnidadesMedidasDropdown = await PopularUnidadesMedidasDropdown();
            alimentoViewModel.GruposAlimentosDropdown = await PopularGruposAlimentosDropdown();

            if (!ModelState.IsValid) return View(alimentoViewModel);

            alimentoDb.Descricao = alimentoViewModel.Descricao;
            alimentoDb.Carboidratos = alimentoViewModel.Carboidratos;
            alimentoDb.Proteinas = alimentoViewModel.Proteinas;
            alimentoDb.Gorduras = alimentoViewModel.Gorduras;
            alimentoDb.Calorias = CalcularCalorias(alimentoViewModel);
            alimentoDb.Porcao = alimentoViewModel.Porcao;
            alimentoDb.GrupoAlimentoId = alimentoViewModel.GrupoAlimentoId;

            await _alimentoService.Atualizar(_mapper.Map<Alimento>(alimentoDb));

            if (!OperacaoValida()) return View(alimentoViewModel);

            return RedirectToAction("Index");
        }

        [Route("remover-alimento/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var alimentoViewModel = await ObterAlimento(id);

            if (alimentoViewModel == null) return NotFound();

            return View(alimentoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remover-alimento/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alimentoViewModel = await ObterAlimento(id);

            if (alimentoViewModel == null) return NotFound();

            await _alimentoService.Remover(id);

            if (!OperacaoValida()) return View(alimentoViewModel);
            
            return RedirectToAction("Index");
        }

        [Route("adicionar-alimento-substituto/{id:guid}")]
        public async Task<IActionResult> AdicionarAlimentoSubstituto(Guid id)
        {
            return PartialView("_AdicionarSubstituicao", new AlimentoSubstituicaoViewModel { AlimentoId = id });
        }

        [HttpPost]
        [Route("adicionar-alimento-substituto/{id:guid}")]
        public async Task<IActionResult> AdicionarAlimentoSubstituto(Guid id, AlimentoSubstituicaoViewModel alimentoSubstituicaoViewModel)
        {
            if (id != alimentoSubstituicaoViewModel.AlimentoId) return NotFound();

            ModelState.Remove("AlimentoSubstituto.Porcao");
            ModelState.Remove("AlimentoSubstituto.Gorduras");
            ModelState.Remove("AlimentoSubstituto.Proteinas");
            ModelState.Remove("AlimentoSubstituto.Carboidratos");

            if (!ModelState.IsValid) return PartialView("_AdicionarSubstituicao", alimentoSubstituicaoViewModel);

            var alimentoSubstituto = _alimentoRepository.Buscar(a => a.Descricao == alimentoSubstituicaoViewModel.AlimentoSubstituto.Descricao).Result.FirstOrDefault();

            if (alimentoSubstituto == null)
            {
                ModelState.AddModelError(string.Empty, "O alimento informado não possui cadastro.");
                return PartialView("_AdicionarSubstituicao", alimentoSubstituicaoViewModel);
            }

            alimentoSubstituicaoViewModel.AlimentoSubstitutoId = alimentoSubstituto.Id;
            alimentoSubstituicaoViewModel.Id = Guid.NewGuid();

            await _alimentoSubstituicaoService.Adicionar(_mapper.Map<AlimentoSubstituicao>(alimentoSubstituicaoViewModel));

            if (!OperacaoValida()) return PartialView("_AdicionarSubstituicao", alimentoSubstituicaoViewModel);

            var url = Url.Action("ObterSubstituicoes", "Alimentos", new { id });
            return Json(new { success = true, url });
        }

        [Route("remover-alimento-substituto/{id:guid}")]
        public async Task<IActionResult> RemoverAlimentoSubstituto(Guid id)
        {
            return PartialView("_RemoverSubstituicao", await ObterAlimentoSubstituicao(id));
        }

        [HttpPost, ActionName("RemoverAlimentoSubstituto")]
        [Route("remover-alimento-substituto/{id:guid}")]
        public async Task<IActionResult> RemoverAlimentoSubstitutoConfirmed(Guid id)
        {
            var alimentoSubstituicaoViewModel = await ObterAlimentoSubstituicao(id);

            if (alimentoSubstituicaoViewModel == null) return NotFound();

            await _alimentoSubstituicaoService.Remover(id);

            if (!OperacaoValida()) return PartialView("_RemoverSubstituicao", alimentoSubstituicaoViewModel);

            var url = Url.Action("ObterSubstituicoes", "Alimentos", new { id = alimentoSubstituicaoViewModel.AlimentoId });
            return Json(new { success = true, url });
        }

        [Route("alimento-autocomplete")]
        public async Task<ActionResult> Autocomplete()
        {
            var term = HttpContext.Request.Query["term"].ToString();

            if (term.Length < 4) return NotFound();

            var alimentos = await _alimentoRepository.Buscar(a => a.Descricao.Contains(term));

            return Ok(alimentos.Select(a => a.Descricao).OrderBy(a => a));
        }

        [Route("alimento-autocomplete-unidade-medida")]
        public async Task<ActionResult> AutocompleteUnidadeMedida(string alimentoDescricao)
        {
            var alimento = await _alimentoRepository.ObterAlimentoPorDescricao(alimentoDescricao);

            if (alimento == null) return null;

            return Ok(alimento.UnidadeMedida.Descricao);
        }

        public async Task<IActionResult> ObterSubstituicoes(Guid id)
        {
            var alimentoViewModel = await ObterAlimentoSubstituicoes(id);

            if (alimentoViewModel == null) return NotFound();

            return PartialView("_Substituicoes", alimentoViewModel);
        }

        private async Task<AlimentoViewModel> ObterAlimento(Guid id)
        {
            return _mapper.Map<AlimentoViewModel>(await _alimentoRepository.ObterAlimento(id));
        }

        private async Task<List<UnidadeMedidaViewModel>> PopularUnidadesMedidasDropdown()
        {
            var unidadesMedidas = await _unidadeMedidaRepository.Buscar(u => u.Ativo);

            return _mapper.Map<List<UnidadeMedidaViewModel>>(unidadesMedidas.OrderBy(u => u.Descricao));
        }

        private async Task<List<GrupoAlimentoViewModel>> PopularGruposAlimentosDropdown()
        {
            var gruposAlimentos = await _grupoAlimentoRepository.Buscar(g => g.Ativo);

            return _mapper.Map<List<GrupoAlimentoViewModel>>(gruposAlimentos.OrderBy(g => g.Codigo));
        }

        private async Task<AlimentoViewModel> ObterAlimentoSubstituicoes(Guid id)
        {
            return _mapper.Map<AlimentoViewModel>(await _alimentoRepository.ObterAlimentoSubstituicoes(id));
        }

        private async Task<AlimentoSubstituicaoViewModel> ObterAlimentoSubstituicao(Guid id)
        {
            return _mapper.Map<AlimentoSubstituicaoViewModel>(await _alimentoSubstituicaoRepository.ObterAlimentoSubstituicao(id));
        }

        private decimal CalcularCalorias(AlimentoViewModel alimentoViewModel)
        {
            if (alimentoViewModel.Carboidratos == null
                || alimentoViewModel.Proteinas == null
                || alimentoViewModel.Gorduras == null) return 0;

            return Convert.ToDecimal(((alimentoViewModel.Carboidratos + alimentoViewModel.Proteinas) * 4) + (alimentoViewModel.Gorduras * 9));
        }
    }
}
