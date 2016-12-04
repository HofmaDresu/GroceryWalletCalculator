﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.Models;
using GroceryWalletCalculator.Pages;
using GroceryWalletCalculator.Persistence;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using MvvmHelpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

            ScanItem = new Command(async _ =>
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "PriceTags",
                    Name = "PriceTag.jpg"
                });

                var keys = await Keys.Keys.GetKeys();

                OcrResults text;
                var client = new VisionServiceClient(keys.MicrosoftVisionApiKey);
                using (var photoStream = photo.GetStream())
                {
                    text = await client.RecognizeTextAsync(photoStream);
                }

                var itemName = text.Regions.SelectMany(r => r.Lines.Where(l => l.Words.All(w => IsAllUpper(w.Text))).Select(l => string.Join(" ", l.Words.Select(w => w.Text)))).FirstOrDefault();

                var pureNumericLines = text.Regions.SelectMany(r => r.Lines.Where(l => l.Words.All(w =>
                {
                    double price;
                    return double.TryParse(w.Text, out price);
                })));

                var itemPrice = pureNumericLines.SelectMany(l => l.Words.Select(w =>
                {
                    double price;
                    return double.TryParse(w.Text, out price) ? price : 999999;
                })).Min();

                var foo = 1;
            }, _ => CrossMedia.Current.IsCameraAvailable);
            ManualAddItem = new Command(_ => _nav.PushAsync(new AddManualItemPage(_remainingCash)));
        }
        bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }

        public void RefreshData()
        {
            Cart.Clear();
            Cart.AddRange(Data.Cart.Select(c => new FormattedGroceryItem(c)));
            _remainingCash = _spendingLimit - Cart.Sum(c => c.Price*c.Quantity);
            ScanItem.ChangeCanExecute();
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
