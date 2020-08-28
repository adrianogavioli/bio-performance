using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class Refeicao : Entity
    {
        public int Ordem { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<DietaRefeicaoAlimento> Alimentos { get; set; }
    }
}