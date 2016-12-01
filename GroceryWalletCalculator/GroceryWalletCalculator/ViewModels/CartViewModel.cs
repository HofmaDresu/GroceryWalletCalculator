﻿using System;
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
    public class CartViewModel : BaseViewModel
    {
        private readonly double _spendingLimit;
        private readonly INavigation _nav;

        public CartViewModel(int storeId, double spendingLimit, INavigation nav)
        {
            _spendingLimit = spendingLimit;
            _nav = nav;
            _remainingCash = spendingLimit;
            Title = $"Grocery Cart for {Data.Stores.Single(s => s.Id == storeId).Name}";
            Cart = new ObservableRangeCollection<FormattedGroceryItem>();

            ScanItem = new Command(_ => { }, _ => false);
            ManualAddItem = new Command(_ => _nav.PushAsync(new AddManualItemPage(_remainingCash)));
        }

        public void RefreshData()
        {
            Cart.Clear();
            Cart.AddRange(Data.Cart.Select(c => new FormattedGroceryItem(c)));
            _remainingCash = _spendingLimit - Cart.Sum(c => c.Price*c.Quantity);

            OnPropertyChanged("RemainingCash");
        }

        private double _remainingCash;
        public string RemainingCash => $"{_remainingCash:C2} left";

        public ObservableRangeCollection<FormattedGroceryItem> Cart { get; set; }

        public Command ScanItem { get; protected set; }

        public Command ManualAddItem { get; protected set; }
    }

    public class FormattedGroceryItem
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }

        public string NameAndQuantity => $"{Quantity} {Name}";
        public string FormattedPrice => $"{Price:C2}";

        public FormattedGroceryItem(GroceryItem data)
        {
            Name = data.Name;
            Quantity = data.Quantity;
            Price = data.Price;
        }
    }
}
