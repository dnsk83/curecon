using System;
namespace curecon.Models
{
    public class CurrencyModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FlagUri { get; set; }

        public CurrencyModel()
        {
        }

        public static string SetFlagUri(string alpha2Code)
        {
            return $"https://www.countryflags.io/{alpha2Code}/flat/64.png";
        }
    }
}
