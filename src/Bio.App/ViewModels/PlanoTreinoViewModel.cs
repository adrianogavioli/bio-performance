using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class PlanoTreinoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        public bool Ativo { get; set; }

        public Guid AtendimentoId { get; set; }

        public AtendimentoViewModel Atendimento { get; set; }

        public List<TreinoViewModel> Treinos { get; set; }
    }
}
