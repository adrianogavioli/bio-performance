using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class Treino : Entity
    {
        public int Ordem { get; set; }

        public string Titulo { get; set; }

        public string Observacao { get; set; }

        public bool Ativo { get; set; }

        public Guid PlanoTreinoId { get; set; }

        public PlanoTreino PlanoTreino { get; set; }

        public IEnumerable<TreinoRelGrupoMuscular> GruposMusculares { get; set; }

        public IEnumerable<DiarioPaciente> Diarios { get; set; }

        public IEnumerable<Exercicio> Exercicios { get; set; }
    }
}
