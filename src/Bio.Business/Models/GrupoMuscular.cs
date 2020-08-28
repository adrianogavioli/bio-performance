using System.Collections.Generic;

namespace Bio.Business.Models
{
    public class GrupoMuscular : Entity
    {
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<TreinoRelGrupoMuscular> Treinos { get; set; }

        public IEnumerable<CatalogoExercicio> CatalogoExercicios { get; set; }
    }
}
