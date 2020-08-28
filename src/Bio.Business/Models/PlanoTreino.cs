using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class PlanoTreino : Entity
    {
        public DateTime DataCadastro { get; set; }

        public string Observacao { get; set; }

        public bool Ativo { get; set; }

        public Guid AtendimentoId { get; set; }

        public Atendimento Atendimento { get; set; }

        public IEnumerable<Treino> Treinos { get; set; }
    }
}
