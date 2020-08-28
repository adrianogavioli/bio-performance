using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class AlimentoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="O campo {0} deve ser preenchido")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Carboidratos (g)")]
        public decimal? Carboidratos { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Proteínas (g)")]
        public decimal? Proteinas { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Gorduras (g)")]
        public decimal? Gorduras { get; set; }

        public decimal Calorias { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Porção")]
        public decimal? Porcao { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Unidade de Medida")]
        public Guid UnidadeMedidaId { get; set; }

        public UnidadeMedidaViewModel UnidadeMedida { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Grupo de Alimento")]
        public Guid GrupoAlimentoId { get; set; }

        public GrupoAlimentoViewModel GrupoAlimento { get; set; }

        public List<AlimentoSubstituicaoViewModel> AlimentosSubstituicoes { get; set; }

        public List<UnidadeMedidaViewModel> UnidadesMedidasDropdown { get; set; }

        public List<GrupoAlimentoViewModel> GruposAlimentosDropdown { get; set; }
    }
}