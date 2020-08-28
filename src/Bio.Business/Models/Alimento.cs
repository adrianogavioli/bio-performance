using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class Alimento : Entity
    {
        public string Descricao { get; set; }

        public decimal Carboidratos { get; set; }

        public decimal Proteinas { get; set; }

        public decimal Gorduras { get; set; }

        public decimal Calorias { get; set; }

        public decimal Porcao { get; set; }

        public Guid UnidadeMedidaId { get; set; }

        public UnidadeMedida UnidadeMedida { get; set; }

        public Guid GrupoAlimentoId { get; set; }

        public GrupoAlimento GrupoAlimento { get; set; }

        public IEnumerable<AlimentoSubstituicao> AlimentosSubstituicoes { get; set; }

        public IEnumerable<AlimentoSubstituicao> AlimentosSubstitutos{ get; set; }

        public IEnumerable<DietaRefeicaoAlimento> DietasRefeicoesAlimentos { get; set; }

        public IEnumerable<DietaRefeicaoSubstituicao> DietasRefeicoesSubstituicoes { get; set; }
    }
}