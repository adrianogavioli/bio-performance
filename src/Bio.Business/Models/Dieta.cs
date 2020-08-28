using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class Dieta : Entity
    {
        public DateTime DataCadastro { get; set; }

        public uint TaxaMetabolicaBasal { get; set; }

        public string Observacao { get; set; }

        public bool Ativo { get; set; }

        public Guid AtendimentoId { get; set; }

        public Atendimento Atendimento { get; set; }

        public IEnumerable<DietaRefeicaoAlimento> Refeicoes { get; set; }
    }
}