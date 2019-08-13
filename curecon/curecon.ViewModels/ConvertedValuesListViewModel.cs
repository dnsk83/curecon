using curecon.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace curecon.ViewModels
{
    public class ConvertedValuesListViewModel : INotifyPropertyChanged
    {
        public event EventHandler AddCurrencyRequested;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ConvertedValueViewModel> ConvertedValuesList { get; set; }
        public Command AddCurrencyCommand { get; set; }

        public ConvertedValuesListViewModel()
        {
            ConvertedValuesList = new ObservableCollection<ConvertedValueViewModel>();
            LoadData();
            OnPropertyChanged(nameof(ConvertedValuesList));
            AddCurrencyCommand = new Command(RequestAddCurrency);
        }

        public void Convert(ConvertedValueViewModel currencyFrom)
        {
            if (ConvertedValuesList.Count <= 1)
            {
                return;
            }
            if (currencyFrom.Rate == 0)
            {
                return;
            }
            foreach (var currency in ConvertedValuesList)
            {
                if (currency == currencyFrom)
                {
                    continue;
                }
                currency.Value = currencyFrom.Value / currencyFrom.Rate * currency.Rate;
                currency.Value = Math.Round(currency.Value, 7);
                if (currency.Value > 0.00001) currency.Value = Math.Round(currency.Value, 5);
                if (currency.Value > 0.0001) currency.Value = Math.Round(currency.Value, 4);
                if (currency.Value > 0.001) currency.Value = Math.Round(currency.Value, 3);
                if (currency.Value > 0.01) currency.Value = Math.Round(currency.Value, 2);
                currency.OnPropertyChanged(nameof(currency.Value));
            }
        }


        private void RequestAddCurrency()
        {
            AddCurrencyRequested?.Invoke(this, new EventArgs());
        }

        public async void AddCurrency(CurrencyViewModel currencyViewModel)
        {
            var service = new RateService();
            var baseCurrencyCode = ConvertedValuesList.Count > 0 ? ConvertedValuesList[0].Code : currencyViewModel.Code;
            var newCurVM = new ConvertedValueViewModel()
            {
                Rate = await service.GetRateAsync(baseCurrencyCode, currencyViewModel.Code),
                Code = currencyViewModel.Code,
                FlagUri = currencyViewModel.FlagUri,
                Value = 0
            };
            ConvertedValuesList.Add(newCurVM);
            OnPropertyChanged(nameof(ConvertedValuesList));
        }

        private void LoadData()
        {
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
