using System;

namespace Bio.Business.Models
{
    public class AlimentoSubstituicao : Entity
    {
        public Guid AlimentoId { get; set; }

        public Alimento Alimento { get; set; }

        public Guid AlimentoSubstitutoId { get; set; }

        public Alimento AlimentoSubstituto { get; set; }
    }
}