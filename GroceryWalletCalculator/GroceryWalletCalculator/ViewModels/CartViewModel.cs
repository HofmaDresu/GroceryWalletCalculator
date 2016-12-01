﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.Models;
using GroceryWalletCalculator.Persistence;
using MvvmHelpers;
using Xamarin.Forms;

namespace GroceryWalletCalculator.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private double _spendingLimit;

        public CartViewModel(int storeId, double spendingLimit)
        {
            _spendingLimit = spendingLimit;
            Title = $"Grocery Cart for {Data.Stores.Single(s => s.Id == storeId).Name}";
            Cart = new ObservableRangeCollection<GroceryItem>();

            ScanItem = new Command(_ => { }, _ => false);
            ManualAddItem = new Command(_ => { }, _ => false);
        }

        private double _remainingCash;
        public double RemainingCash
        {
            get { return _remainingCash; }
            set { SetProperty(ref _remainingCash, value); }
        }

        public ObservableRangeCollection<GroceryItem> Cart { get; set; }

        public Command ScanItem { get; protected set; }

        public Command ManualAddItem { get; protected set; }
    }
}
