using curecon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace curecon.ViewModels
{
    public class ConvertedValueViewModel : INotifyPropertyChanged
    {
        public double Rate { get; set; }
        public string Code {get;set;}
        public double Value { get; set; }
        public string FlagUri { get; set; }

        public ConvertedValueViewModel()
        { 
        }

        public event PropertyChangedEventHandler PropertyChanged;



        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
