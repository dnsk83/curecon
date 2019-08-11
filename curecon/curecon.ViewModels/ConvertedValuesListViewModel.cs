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
            AddCurrencyCommand = new Command(OnAddCurrency);
        }

        private void OnAddCurrency()
        {
            AddCurrencyRequested?.Invoke(this, new EventArgs());
        }

        private void LoadData()
        {
            ConvertedValuesList.Add(new ConvertedValueViewModel() { Code = "RUB", FlagUri = "https://www.countryflags.io/be/flat/64.png", Value = 1m });
            ConvertedValuesList.Add(new ConvertedValueViewModel() { Code = "USD", FlagUri = "https://www.countryflags.io/ae/flat/64.png", Value = 1.23m });
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
