using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using curecon.Core;
using curecon.Models;

namespace curecon.ViewModels
{
    public class CurrencyListViewModel : INotifyPropertyChanged
    {
        bool IsFilterBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public CurrencyListModel Converter { get; set; }
        public ObservableCollection<CurrencyViewModel> Currencies { get; set; }
        public ObservableCollection<CurrencyViewModel> FilteredCurrencies { get; set; }

        public CurrencyListViewModel()
        {
            Currencies = new ObservableCollection<CurrencyViewModel>();
            FilteredCurrencies = new ObservableCollection<CurrencyViewModel>();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await LoadModelAsync();

            foreach (var model in Converter.CurrencyModels)
            {
                Currencies.Add(new CurrencyViewModel() { Code = model.Code, Name = model.Name, FlagUri = model.FlagUri });
            }
            foreach (var item in Currencies)
            {
                FilteredCurrencies.Add(item);
            }

            OnPropertyChanged(nameof(FilteredCurrencies));
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

        public async void OnFilterChanged(string substring)
        {
            if (IsFilterBusy)
            {
                return;
            }
            IsFilterBusy = true;
            await Task.Run(() =>
            {
                if (substring != string.Empty)
                {
                    FilteredCurrencies.Clear();
                    foreach (var item in Currencies)
                    {
                        if (item.Name == null) continue;
                        if (item.Code == null) continue;
                        if (item.Name.ToLower().Contains(substring.ToLower()) || item.Code.ToLower().Contains(substring.ToLower()))
                        {
                            FilteredCurrencies.Add(item);
                        }
                    }
                }
                else
                {
                    FilteredCurrencies.Clear();
                    foreach (var item in Currencies)
                    {
                        FilteredCurrencies.Add(item);
                    }
                }

                OnPropertyChanged(nameof(FilteredCurrencies));
            });
            IsFilterBusy = false;
        }
    }
}
