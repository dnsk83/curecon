using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace curecon.ViewModels
{
    public class ConvertedValuesListViewModel : INotifyPropertyChanged
    {
        public event EventHandler AddCurrencyRequested;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ConvertedValueViewModel> ConvertedValuesList { get; set; }
        public ICommand AddCurrencyCommand { get; set; }

        public ConvertedValuesListViewModel()
        {
            ConvertedValuesList = new ObservableCollection<ConvertedValueViewModel>();
            LoadData();
            OnPropertyChanged(nameof(ConvertedValuesList));
            AddCurrencyCommand = new AddCurrencyCommand(OnAddCurrency);
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

    internal class AddCurrencyCommand : ICommand
    {
        private Action p;

        public AddCurrencyCommand(Action p)
        {
            this.p = p;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            p?.Invoke();
        }
    }
}
