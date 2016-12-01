using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.ViewModels;
using Xamarin.Forms;

namespace GroceryWalletCalculator.Pages
{
    public partial class SelectStorePage : ContentPage
    {
        public SelectStorePage()
        {
            InitializeComponent();
            BindingContext = new SelectStoreViewModel(Navigation);
        }
    }
}
