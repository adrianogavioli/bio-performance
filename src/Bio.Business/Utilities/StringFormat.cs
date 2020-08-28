namespace Bio.Business.Utilities
{
    public class StringFormat
    {
        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = "";

            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }

            return onlyNumber.Trim();
        }
    }
}
