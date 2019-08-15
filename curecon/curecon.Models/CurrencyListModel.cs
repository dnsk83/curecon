using curecon.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace curecon.Models
{
    public class CurrencyListModel
    {
        ICountriesService CountriesService;
        public List<CurrencyModel> CurrencyModels { get; set; }

        public CurrencyListModel()
        {
            CurrencyModels = new List<CurrencyModel>();
            CountriesService = new CountriesService();
        }

        public async Task LoadCurrenciesAsync()
        {
            var countryDtos = await CountriesService.GetCountriesAsync();
            foreach (var dto in countryDtos)
            {
                foreach (var currency in dto.Currencies)
                {
                    CurrencyModels.Add(new CurrencyModel() { Code = currency.Code, Name = currency.Name, FlagUri = CurrencyModel.SetFlagUri(dto.Alpha2Code) });
                }
            }
        }
    }
}
