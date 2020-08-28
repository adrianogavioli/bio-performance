using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class DiarioPacienteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        public decimal? Peso { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Paciente")]
        public Guid PacienteId { get; set; }

        public PacienteViewModel Paciente { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Treino")]
        public Guid TreinoId { get; set; }

        public TreinoViewModel Treino { get; set; }

        public List<PacienteViewModel> PacientesDropdown { get; set; }

        public List<TreinoViewModel> TreinosDropdown { get; set; }
    }
}
