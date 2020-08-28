using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class DietaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Taxa Metabólica Basal (cal)")]
        public uint? TaxaMetabolicaBasal { get; set; }

        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        public bool Ativo { get; set; }

        public Guid AtendimentoId { get; set; }

        public AtendimentoViewModel Atendimento { get; set; }

        [DisplayName("Proteínas Totais (g)")]
        public decimal ProteinasTotais { get; set; }

        [DisplayName("Carboidratos Totais (g)")]
        public decimal CarboidratosTotais { get; set; }

        [DisplayName("Gorduras Totais (g)")]
        public decimal GordurasTotais { get; set; }

        [DisplayName("Calorias Totais")]
        public int CaloriasTotais { get; set; }

        [DisplayName("Diferença Calórica")]
        public int DiferencaCalorica { get; set; }

        public List<DietaRefeicaoAlimentoViewModel> Refeicoes { get; set; }

    }
}