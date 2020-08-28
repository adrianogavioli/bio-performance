using System;
using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class CatalogoExercicio : Entity
    {
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public Guid GrupoMuscularId { get; set; }

        public GrupoMuscular GrupoMuscular { get; set; }

        public IEnumerable<Exercicio> Exercicios { get; set; }
    }
}
