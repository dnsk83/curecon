using curecon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace curecon.FormsUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertedValuesPage : ContentPage
    {
        ConvertedValuesListViewModel VM { get; set; }

        public ConvertedValuesPage()
        {
            InitializeComponent();

            VM = new ConvertedValuesListViewModel();
            this.BindingContext = VM;

            VM.AddCurrencyRequested += VM_AddCurrencyRequested;
        }

        private void VM_AddCurrencyRequested(object sender, EventArgs e)
        {
            var curList = new CurrenciesListPage();
            curList.CurrencySelected += (s, curSelectedEvtArgs) =>
              {
                  VM.AddCurrency(curSelectedEvtArgs.CurrencyViewModel);
              };
            this.Navigation.PushAsync(curList);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((Entry)sender).IsFocused)
            {
                var selectedCurrency = (ConvertedValueViewModel)((Entry)sender).BindingContext;
                VM.Convert(selectedCurrency);
            }
        }
    }
}