using Bio.App.ViewModels;
using System;
using System.Linq;

namespace Bio.App.Utilities
{
    public static class DietaCalculos
    {
        private static int CalcularCaloriasRefeicao(decimal alimentoCalorias, decimal alimentoPorcao, decimal refeicaoQuantidade)
        {
            return Convert.ToInt32((alimentoCalorias / alimentoPorcao) * refeicaoQuantidade);
        }

        private static decimal CalcularProteinasRefeicao(decimal alimentoProteinas, decimal alimentoPorcao, decimal refeicaoQuantidade)
        {
            return (alimentoProteinas / alimentoPorcao) * refeicaoQuantidade;
        }

        private static decimal CalcularCarboidratosRefeicao(decimal alimentoCarboidratos, decimal alimentoPorcao, decimal refeicaoQuantidade)
        {
            return (alimentoCarboidratos / alimentoPorcao) * refeicaoQuantidade;
        }

        private static decimal CalcularGordurasRefeicao(decimal alimentoGorduras, decimal alimentoPorcao, decimal refeicaoQuantidade)
        {
            return (alimentoGorduras / alimentoPorcao) * refeicaoQuantidade;
        }

        public static DietaRefeicaoAlimentoViewModel CalcularValoresNutricionais(DietaRefeicaoAlimentoViewModel dietaRefeicaoAlimentoViewModel)
        {
            var alimentoCalorias = dietaRefeicaoAlimentoViewModel.Alimento.Calorias;
            var alimentoProteinas = Convert.ToDecimal(dietaRefeicaoAlimentoViewModel.Alimento.Proteinas);
            var alimentoCarboidratos = Convert.ToDecimal(dietaRefeicaoAlimentoViewModel.Alimento.Carboidratos);
            var alimentoGordura = Convert.ToDecimal(dietaRefeicaoAlimentoViewModel.Alimento.Gorduras);
            var alimentoPorcao = Convert.ToDecimal(dietaRefeicaoAlimentoViewModel.Alimento.Porcao);
            var refeicaoQuantidade = Convert.ToDecimal(dietaRefeicaoAlimentoViewModel.Quantidade);

            dietaRefeicaoAlimentoViewModel.Calorias = CalcularCaloriasRefeicao(alimentoCalorias, alimentoPorcao, refeicaoQuantidade);
            dietaRefeicaoAlimentoViewModel.Proteinas = CalcularProteinasRefeicao(alimentoProteinas, alimentoPorcao, refeicaoQuantidade);
            dietaRefeicaoAlimentoViewModel.Carboidratos = CalcularCarboidratosRefeicao(alimentoCarboidratos, alimentoPorcao, refeicaoQuantidade);
            dietaRefeicaoAlimentoViewModel.Gorduras = CalcularGordurasRefeicao(alimentoGordura, alimentoPorcao, refeicaoQuantidade);

            return dietaRefeicaoAlimentoViewModel;
        }

        public static DietaRefeicaoSubstituicaoViewModel CalcularValoresNutricionaisSubstituicao(DietaRefeicaoSubstituicaoViewModel dietaRefeicaoSubstituicaoViewModel)
        {
            var alimentoCalorias = dietaRefeicaoSubstituicaoViewModel.Alimento.Calorias;
            var alimentoProteinas = Convert.ToDecimal(dietaRefeicaoSubstituicaoViewModel.Alimento.Proteinas);
            var alimentoCarboidratos = Convert.ToDecimal(dietaRefeicaoSubstituicaoViewModel.Alimento.Carboidratos);
            var alimentoGordura = Convert.ToDecimal(dietaRefeicaoSubstituicaoViewModel.Alimento.Gorduras);
            var alimentoPorcao = Convert.ToDecimal(dietaRefeicaoSubstituicaoViewModel.Alimento.Porcao);
            var refeicaoQuantidade = Convert.ToDecimal(dietaRefeicaoSubstituicaoViewModel.Quantidade);

            dietaRefeicaoSubstituicaoViewModel.Calorias = CalcularCaloriasRefeicao(alimentoCalorias, alimentoPorcao, refeicaoQuantidade);
            dietaRefeicaoSubstituicaoViewModel.Proteinas = CalcularProteinasRefeicao(alimentoProteinas, alimentoPorcao, refeicaoQuantidade);
            dietaRefeicaoSubstituicaoViewModel.Carboidratos = CalcularCarboidratosRefeicao(alimentoCarboidratos, alimentoPorcao, refeicaoQuantidade);
            dietaRefeicaoSubstituicaoViewModel.Gorduras = CalcularGordurasRefeicao(alimentoGordura, alimentoPorcao, refeicaoQuantidade);

            return dietaRefeicaoSubstituicaoViewModel;
        }

        private static DietaViewModel CalcularProteinasTotais(DietaViewModel dietaViewModel)
        {
            dietaViewModel.ProteinasTotais = dietaViewModel.Refeicoes.Sum(r => r.Proteinas);

            return dietaViewModel;
        }

        private static DietaViewModel CalcularCarboidratosTotais(DietaViewModel dietaViewModel)
        {
            dietaViewModel.CarboidratosTotais = dietaViewModel.Refeicoes.Sum(r => r.Carboidratos);

            return dietaViewModel;
        }

        private static DietaViewModel CalcularGordurasTotais(DietaViewModel dietaViewModel)
        {
            dietaViewModel.GordurasTotais = dietaViewModel.Refeicoes.Sum(r => r.Gorduras);

            return dietaViewModel;
        }

        private static DietaViewModel CalcularCaloriasTotais(DietaViewModel dietaViewModel)
        {
            dietaViewModel.CaloriasTotais = dietaViewModel.Refeicoes.Sum(r => r.Calorias);

            return dietaViewModel;
        }

        public static DietaViewModel CalcularValoresNutricionaisTotais(DietaViewModel dietaViewModel)
        {
            CalcularProteinasTotais(dietaViewModel);
            CalcularCarboidratosTotais(dietaViewModel);
            CalcularGordurasTotais(dietaViewModel);
            CalcularCaloriasTotais(dietaViewModel);

            return dietaViewModel;
        }

        public static DietaViewModel CalcularDiferencaCalorica(DietaViewModel dietaViewModel)
        {
            dietaViewModel.DiferencaCalorica = dietaViewModel.CaloriasTotais - Convert.ToInt32(dietaViewModel.TaxaMetabolicaBasal);

            return dietaViewModel;
        }

        public static DietaRefeicaoSubstituicaoViewModel CalcularQuantidadeSubstituicao(DietaRefeicaoSubstituicaoViewModel dietaRefeicaoSubstituicaoViewModel)
        {
            var caloriasRefeicao = dietaRefeicaoSubstituicaoViewModel.DietaRefeicaoAlimento.Calorias;
            var caloriasAlimento = dietaRefeicaoSubstituicaoViewModel.Alimento.Calorias;
            var porcaoAlimento = dietaRefeicaoSubstituicaoViewModel.Alimento.Porcao;

            dietaRefeicaoSubstituicaoViewModel.Quantidade = caloriasRefeicao / (caloriasAlimento / porcaoAlimento);

            return dietaRefeicaoSubstituicaoViewModel;
        }
    }
}
