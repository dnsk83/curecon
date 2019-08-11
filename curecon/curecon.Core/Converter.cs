using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace curecon.Core
{
    public class Converter
    {
        public List<CurrencyModel> CurrencyModels { get; set; }

        public Converter()
        {
            CurrencyModels = new List<CurrencyModel>();
        }

        public async Task LoadCurrenciesAsync()
        {
            var service = new CountriesService();
            var countryDtos = await service.GetCountriesAsync();
            foreach (var dto in countryDtos)
            {
                foreach (var currency in dto.Currencies)
                {
                    CurrencyModels.Add(new CurrencyModel() { Code = currency.Code, Name = currency.Name, FlagUri = GetFlagUri(dto.Alpha2Code) });
                }
            }
        }

        // TODO: move to currency model
        private string GetFlagUri(string alpha2Code)
        {
            return $"https://www.countryflags.io/{alpha2Code}/flat/64.png";
        }
    }
}
