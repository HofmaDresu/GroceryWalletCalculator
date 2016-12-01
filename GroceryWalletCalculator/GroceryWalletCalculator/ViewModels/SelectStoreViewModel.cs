using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.Models;
using GroceryWalletCalculator.Persistence;
using MvvmHelpers;

namespace GroceryWalletCalculator.ViewModels
{
    public class SelectStoreViewModel : BaseViewModel
    {
        public SelectStoreViewModel()
        {
            Stores = new ObservableRangeCollection<Store>();
            Stores.AddRange(Data.Stores);
        }

        public ObservableRangeCollection<Store> Stores { get; set; }
    }
}
