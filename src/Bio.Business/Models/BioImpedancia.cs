using System;

namespace Bio.Business.Models
{
    public class BioImpedancia : Entity
    {
        public DateTime DataCadastro { get; set; }

        public decimal AguaCorporal { get; set; }

        public decimal GorduraCorporal { get; set; }

        public decimal Proteinas { get; set; }

        public decimal Minerais { get; set; }

        public uint TaxaMetabolicaBasal { get; set; }

        public Guid PacienteId { get; set; }

        public Paciente Paciente { get; set; }
    }
}
