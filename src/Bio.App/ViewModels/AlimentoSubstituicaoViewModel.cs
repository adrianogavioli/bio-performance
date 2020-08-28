using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class AlimentoSubstituicaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [HiddenInput]
        public Guid AlimentoId { get; set; }

        public AlimentoViewModel Alimento { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser preenchido")]
        [DisplayName("Alimento Substituto")]
        public Guid AlimentoSubstitutoId { get; set; }

        public AlimentoViewModel AlimentoSubstituto { get; set; }
    }
}