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
        }
    }
}