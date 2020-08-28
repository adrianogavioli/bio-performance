using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class DietaRefeicaoAlimentoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal? Quantidade { get; set; }

        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        [HiddenInput]
        public Guid DietaId { get; set; }

        public DietaViewModel Dieta { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Refeição")]
        public Guid? RefeicaoId { get; set; }

        public RefeicaoViewModel Refeicao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Alimento")]
        public Guid AlimentoId { get; set; }

        public AlimentoViewModel Alimento { get; set; }

        public int Calorias { get; set; }

        [DisplayName("Proteínas (g)")]
        public decimal Proteinas { get; set; }

        [DisplayName("Carboidratos (g)")]
        public decimal Carboidratos { get; set; }

        [DisplayName("Gorduras (g)")]
        public decimal Gorduras { get; set; }

        public List<DietaRefeicaoSubstituicaoViewModel> DietasRefeicoesSubstituicoes { get; set; }

        public List<RefeicaoViewModel> RefeicoesDropdown { get; set; }
    }
}