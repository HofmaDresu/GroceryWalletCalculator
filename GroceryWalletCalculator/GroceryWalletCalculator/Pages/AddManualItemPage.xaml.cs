using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.ViewModels;
using Xamarin.Forms;

namespace GroceryWalletCalculator.Pages
{
    public partial class AddManualItemPage : ContentPage
    {
        public AddManualItemPage(double remainingLimit)
        {
            InitializeComponent();
            BindingContext = new AddManualItemViewModel(remainingLimit, Navigation);
        }
    }
}
