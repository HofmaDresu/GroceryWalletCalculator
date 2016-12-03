using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.Models;
using GroceryWalletCalculator.Pages;
using GroceryWalletCalculator.Persistence;
using MvvmHelpers;
using Xamarin.Forms;

namespace GroceryWalletCalculator.ViewModels
{
    public class SelectStoreViewModel : BaseViewModel
    {
        private readonly INavigation _nav;

        public SelectStoreViewModel(INavigation nav)
        {
            _nav = nav;
            Stores = new ObservableRangeCollection<Store>();
            Stores.AddRange(Data.Stores);
        }

        public ObservableRangeCollection<Store> Stores { get; set; }


        private Store _selectedStore;
        public Store SelectedStore
        {
            get { return _selectedStore; }
            set
            {
                if (value == null) return;
                _nav.PushAsync(new SetSpendingLimitPage(value.Id));

                SetProperty(ref _selectedStore, value);
                SetProperty(ref _selectedStore, null);
            }
        }
    }
}
