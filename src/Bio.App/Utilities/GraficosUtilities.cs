namespace Bio.App.Utilities
{
    public static class GraficosUtilities
    {
        public static string TratarDecimais(decimal numero)
        {
            return numero.ToString().Replace(".", "").Replace(",", ".");
        }
    }
}
