using System;

namespace Bio.Business.Models
{
    public class DietaRefeicaoSubstituicao : Entity
    {
        public decimal Quantidade { get; set; }

        public string Observacao { get; set; }

        public Guid DietaRefeicaoAlimentoId { get; set; }

        public DietaRefeicaoAlimento DietaRefeicaoAlimento { get; set; }

        public Guid AlimentoId { get; set; }

        public Alimento Alimento { get; set; }
    }
}