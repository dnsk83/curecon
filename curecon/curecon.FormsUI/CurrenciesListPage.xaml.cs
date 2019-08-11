using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using curecon.Core;
using curecon.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace curecon.FormsUI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class CurrenciesListPage : ContentPage
    {
        public CurrencyListViewModel VM { get; set; }
        public event EventHandler<CurrencySelectedEventArgs> CurrencySelected;

        public CurrenciesListPage()
        {
            InitializeComponent();

            VM = new CurrencyListViewModel();
            this.BindingContext = VM;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CurrencySelected?.Invoke(this, new CurrencySelectedEventArgs((CurrencyViewModel)e.SelectedItem));
            Navigation.PopAsync();
        }
    }

    public class CurrencySelectedEventArgs
    {
        public CurrencyViewModel CurrencyViewModel { get; set; }

        public CurrencySelectedEventArgs(CurrencyViewModel currencyViewModel)
        {
            CurrencyViewModel=currencyViewModel;
        }
    }
}
