using System;

namespace Bio.Business.Models
{
    public class DiarioPaciente : Entity
    {
        public DateTime DataCadastro { get; set; }

        public decimal Peso { get; set; }

        public string Observacao { get; set; }

        public Guid PacienteId { get; set; }

        public Paciente Paciente { get; set; }

        public Guid TreinoId { get; set; }

        public Treino Treino { get; set; }
    }
}
