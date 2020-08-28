using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class DietaRefeicaoAlimento : Entity
    {
        public decimal Quantidade { get; set; }

        public string Observacao { get; set; }

        public Guid DietaId { get; set; }

        public Dieta Dieta { get; set; }

        public Guid RefeicaoId { get; set; }

        public Refeicao Refeicao { get; set; }

        public Guid AlimentoId { get; set; }

        public Alimento Alimento { get; set; }

        public IEnumerable<DietaRefeicaoSubstituicao> DietasRefeicoesSubstituicoes { get; set; }
    }
}