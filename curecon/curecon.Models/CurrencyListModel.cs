using curecon.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace curecon.Models
{
    public class CurrencyListModel
    {
        public List<CurrencyModel> CurrencyModels { get; set; }

        public CurrencyListModel()
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
                    CurrencyModels.Add(new CurrencyModel() { Code = currency.Code, Name = currency.Name, FlagUri = CurrencyModel.SetFlagUri(dto.Alpha2Code) });
                }
            }
        }
    }
}
