using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class BioImpedanciaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Água Corporal")]
        public decimal AguaCorporal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Gordura Corporal")]
        public decimal GorduraCorporal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Proteínas")]
        public decimal Proteinas { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Minerais { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Taxa Metabólica Basal (cal)")]
        public uint TaxaMetabolicaBasal { get; set; }

        [DisplayName("Massa Magra")]
        public decimal MassaMagra { get; set; }

        public decimal Peso { get; set; }

        [DisplayName("% Gordura")]
        public decimal PercentGordura { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Paciente")]
        public Guid PacienteId { get; set; }

        public PacienteViewModel Paciente { get; set; }

        public List<PacienteViewModel> PacientesDropdown { get; set; }
    }
}
