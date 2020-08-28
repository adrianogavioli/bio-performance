using Bio.App.ViewModels;

namespace Bio.App.Utilities
{
    public static class BioimpedanciaCalculos
    {
        public static BioImpedanciaViewModel CalcularDadosAdicionais(BioImpedanciaViewModel bioImpedanciaViewModel)
        {
            bioImpedanciaViewModel.MassaMagra = bioImpedanciaViewModel.AguaCorporal + bioImpedanciaViewModel.Proteinas + bioImpedanciaViewModel.Minerais;
            bioImpedanciaViewModel.Peso = bioImpedanciaViewModel.MassaMagra + bioImpedanciaViewModel.GorduraCorporal;
            bioImpedanciaViewModel.PercentGordura = (bioImpedanciaViewModel.GorduraCorporal / bioImpedanciaViewModel.Peso) * 100;

            return bioImpedanciaViewModel;
        }
    }
}
