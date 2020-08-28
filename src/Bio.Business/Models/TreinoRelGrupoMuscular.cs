using System;

namespace Bio.Business.Models
{
    public class TreinoRelGrupoMuscular : Entity
    {
        public Guid TreinoId { get; set; }

        public Treino Treino { get; set; }

        public Guid GrupoMuscularId { get; set; }

        public GrupoMuscular GrupoMuscular { get; set; }
    }
}
