using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class TreinoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        public int? Ordem { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        public bool Ativo { get; set; }

        public Guid PlanoTreinoId { get; set; }

        public PlanoTreinoViewModel PlanoTreino { get; set; }

        public List<TreinoRelGrupoMuscularViewModel> GruposMusculares { get; set; }

        public List<DiarioPacienteViewModel> Diarios { get; set; }

        public List<ExercicioViewModel> Exercicios { get; set; }

        public List<GrupoMuscularViewModel> GruposMuscularesDropdown { get; set; }

        [DisplayName("Grupos Musculares")]
        public string GruposMuscularesConcats { get; set; }
    }
}
