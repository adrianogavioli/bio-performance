using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class GrupoAlimento : Entity
    {
        public uint Codigo { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<Alimento> Alimentos { get; set; }
    }
}
