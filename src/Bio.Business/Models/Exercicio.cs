using System;

namespace Bio.Business.Models
{
    public class Exercicio : Entity
    {
        public decimal Ordem { get; set; }

        public string Repeticao { get; set; }

        public string Observacao { get; set; }

        public Guid TreinoId { get; set; }

        public Treino Treino { get; set; }

        public Guid CatalogoExercicioId { get; set; }

        public CatalogoExercicio CatalogoExercicio { get; set; }
    }
}
