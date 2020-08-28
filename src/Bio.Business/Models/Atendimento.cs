using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class Atendimento : Entity
    {
        public DateTime DataCadastro { get; set; }

        public string Objetivo { get; set; }

        public string RestricaoMedica { get; set; }

        public string RestricaoAlimentar { get; set; }

        public string Observacao { get; set; }

        public Guid ProfissionalId { get; set; }

        public Profissional Profissional { get; set; }

        public Guid PacienteId { get; set; }

        public Paciente Paciente { get; set; }

        public IEnumerable<Dieta> Dietas { get; set; }

        public IEnumerable<PlanoTreino> PlanosTreinos { get; set; }
    }
}
