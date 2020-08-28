using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class UnidadeMedida : Entity
    {
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<Alimento> Alimentos { get; set; }
    }
}
