using curecon.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace curecon.ViewModels
{
    public class ConvertedValuesListViewModel : INotifyPropertyChanged
    {
        private bool Initialized;

        public event EventHandler AddCurrencyRequested;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ConvertedValueViewModel> ConvertedValuesList { get; set; }
        public Command AddCurrencyCommand { get; set; }

        public ConvertedValuesListViewModel()
        {
            ConvertedValuesList = new ObservableCollection<ConvertedValueViewModel>();
            AddCurrencyCommand = new Command(RequestAddCurrency);
        }

        public void OnAppearing()
        {
            if (!Initialized)
            {
                LoadListAsync();
                Initialized = true;
            }
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
            SaveListAsync();
        }

        private async Task SaveListAsync()
        {
            await Task.Run(() =>
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ConvertedValueViewModel[]));
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "listtoconvert.xml");
                using (var writer = new StreamWriter(path))
                {
                    var values = new ConvertedValueViewModel[ConvertedValuesList.Count];
                    for (int i = 0; i < ConvertedValuesList.Count; i++)
                    {
                        values[i] = ConvertedValuesList[i];
                    }
                    formatter.Serialize(writer, values);
                }
            });
        }

        private async Task LoadListAsync()
        {
            await Task.Run(() =>
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ConvertedValueViewModel[]));
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "listtoconvert.xml");
                using (var fs = new StreamReader(path))
                {
                    ConvertedValueViewModel[] loadedList = (ConvertedValueViewModel[])formatter.Deserialize(fs);
                    foreach (ConvertedValueViewModel newCurVM in loadedList)
                    {
                        ConvertedValuesList.Add(newCurVM);
                    }
                }
            });
            OnPropertyChanged(nameof(ConvertedValuesList));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
