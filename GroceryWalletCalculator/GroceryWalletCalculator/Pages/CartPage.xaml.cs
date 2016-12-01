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
        private CartViewModel _vm;

        public CartPage(int storeId, double spendingLimit)
        {
            InitializeComponent();
            BindingContext = _vm = new CartViewModel(storeId, spendingLimit);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.RefreshData();
        }
    }
}
