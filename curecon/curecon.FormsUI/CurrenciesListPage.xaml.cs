using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using curecon.Core;
using curecon.ViewModels;
using Xamarin.Forms;

namespace curecon
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class CurrenciesListPage : ContentPage
    {
        public CurrencyListViewModel VM { get; set; }

        public CurrenciesListPage()
        {
            InitializeComponent();

            VM = new CurrencyListViewModel();
            this.BindingContext = VM;
        }
    }
}
