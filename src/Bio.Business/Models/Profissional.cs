using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class Profissional : Entity
    {
        public string Nome { get; set; }

        public string Documento { get; set; }

        public string Especialidade { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<Atendimento> Atendimentos { get; set; }
    }
}
