using System;
using System.ComponentModel.DataAnnotations;

namespace Bio.App.ViewModels
{
    public class TreinoRelGrupoMuscularViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TreinoId { get; set; }

        public TreinoViewModel Treino { get; set; }

        public Guid GrupoMuscularId { get; set; }

        public GrupoMuscularViewModel GrupoMuscular { get; set; }
    }
}
