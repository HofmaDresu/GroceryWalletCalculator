using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.ViewModels;
using Xamarin.Forms;

namespace GroceryWalletCalculator.Pages
{
    public partial class SetSpendingLimitPage : ContentPage
    {
        public SetSpendingLimitPage(int storeId)
        {
            InitializeComponent();
            BindingContext = new SetSpendingLimitViewModel(storeId);
        }
    }
}
