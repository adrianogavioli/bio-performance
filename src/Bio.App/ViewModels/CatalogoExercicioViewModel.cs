using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class CatalogoExercicioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Grupo Muscular")]
        public Guid GrupoMuscularId { get; set; }

        public GrupoMuscularViewModel GrupoMuscular { get; set; }

        public List<GrupoMuscularViewModel> GruposMuscularesDropdown { get; set; }
    }
}
