using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.ViewModels;
using Xamarin.Forms;

namespace GroceryWalletCalculator.Pages
{
    public partial class AddOcrItemPage : ContentPage
    {
        public AddOcrItemPage(double remainingLimit, string ocrItemName, double? ocrItemPrice)
        {
            InitializeComponent();
            BindingContext = new AddOcrItemViewModel(remainingLimit, ocrItemName, ocrItemPrice, Navigation);
        }
    }
}
