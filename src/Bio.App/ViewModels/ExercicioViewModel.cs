using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class ExercicioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        public decimal? Ordem { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        [DisplayName("Repetições")]
        public string Repeticao { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        public Guid TreinoId { get; set; }

        public TreinoViewModel Treino { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Exercício")]
        public Guid CatalogoExercicioId { get; set; }

        public CatalogoExercicioViewModel CatalogoExercicio { get; set; }

        public List<CatalogoExercicioViewModel> CatalogoExerciciosDropdown { get; set; }
    }
}
