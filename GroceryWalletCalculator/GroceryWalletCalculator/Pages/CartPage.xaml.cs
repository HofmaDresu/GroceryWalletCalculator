using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.ViewModels;
using Plugin.Media;
using Xamarin.Forms;

namespace GroceryWalletCalculator.Pages
{
    public partial class CartPage : ContentPage
    {
        private CartViewModel _vm;

        public CartPage(int storeId, double spendingLimit)
        {
            BindingContext = _vm = new CartViewModel(storeId, spendingLimit, Navigation);
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CrossMedia.Current.Initialize();
            _vm.RefreshData();
        }
    }
}
