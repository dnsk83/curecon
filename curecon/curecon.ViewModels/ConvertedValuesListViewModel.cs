using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace curecon.ViewModels
{
    public class ConvertedValuesListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ConvertedValueViewModel> ConvertedValuesList { get; set; }

        public ConvertedValuesListViewModel()
        {
            ConvertedValuesList = new ObservableCollection<ConvertedValueViewModel>();
            LoadData();
            OnPropertyChanged(nameof(ConvertedValuesList));
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
