using Bio.Business.Models.Enums;
using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class Paciente : Entity
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public decimal Altura { get; set; }

        public Genero Genero { get; set; }

        public IEnumerable<BioImpedancia> BioImpedancias { get; set; }

        public IEnumerable<Atendimento> Atendimentos { get; set; }

        public IEnumerable<DiarioPaciente> Diarios { get; set; }
    }
}
