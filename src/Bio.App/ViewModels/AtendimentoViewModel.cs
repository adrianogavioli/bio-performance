using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class AtendimentoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Data")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Objetivo { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        [DisplayName("Restrição Médica")]
        public string RestricaoMedica { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        [DisplayName("Restrição Alimentar")]
        public string RestricaoAlimentar { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Profissional")]
        public Guid ProfissionalId { get; set; }

        public ProfissionalViewModel Profissional { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Paciente")]
        public Guid PacienteId { get; set; }

        public PacienteViewModel Paciente { get; set; }

        public List<DietaViewModel> Dietas { get; set; }

        public List<PlanoTreinoViewModel> PlanosTreinos { get; set; }

        public List<ProfissionalViewModel> ProfissionaisDropdown { get; set; }

        public List<PacienteViewModel> PacientesDropdown { get; set; }
    }
}
