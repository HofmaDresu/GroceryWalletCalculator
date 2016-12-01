using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.ViewModels;
using Xamarin.Forms;

namespace GroceryWalletCalculator.Pages
{
    public partial class CartPage : ContentPage
    {
        public CartPage(int storeId, double spendingLimit)
        {
            InitializeComponent();
            BindingContext = new CartViewModel(storeId, spendingLimit);
        }
    }
}
