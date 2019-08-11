using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using curecon.Core;
using curecon.Models;

namespace curecon.ViewModels
{
    public class CurrencyListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CurrencyListModel Converter { get; set; }
        public ObservableCollection<CurrencyViewModel> Currencies { get; set; }

        public CurrencyListViewModel()
        {
            Currencies = new ObservableCollection<CurrencyViewModel>();

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await LoadModelAsync();

            foreach (var model in Converter.CurrencyModels)
            {
                Currencies.Add(new CurrencyViewModel() { Code = model.Code, Name = model.Name, FlagUri = model.FlagUri });
            }

            OnPropertyChanged(nameof(Currencies));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoadModelAsync()
        {
            Converter = new CurrencyListModel();
            await Converter.LoadCurrenciesAsync();
        }
    }
}
