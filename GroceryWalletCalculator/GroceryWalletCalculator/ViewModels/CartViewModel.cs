using System;
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
using static System.Char;

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
                try
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

                    var itemName = text.Regions.SelectMany(r => r.Lines.Where(l => !AllWordsAreNumeric(l) && l.Words.All(w => IsAllUpper(w.Text) && (!WordContainsNonAlphaNumbers(w) || WordIsNumeric(w))))
                                                .Select(l => string.Join(" ", l.Words.Select(w => w.Text)))).FirstOrDefault();

                    var pureNumericLines = text.Regions.SelectMany(r => r.Lines.Where(AllWordsAreNumeric)).ToList();

                    var prices = pureNumericLines?.SelectMany(l => l?.Words?.Select(w =>
                    {
                        double price;
                        return double.TryParse(w.Text, out price) ? price : 999999;
                    })).ToList();

                    var itemPrice = prices.Any() ? prices.Min() : (double?)null;

                    await _nav.PushAsync(new AddOcrItemPage(_remainingCash, itemName, itemPrice));
                }
                catch (Exception ex)
                {

                    int asdf = 1;
                }
            }, _ => CrossMedia.Current.IsCameraAvailable);

            ManualAddItem = new Command(async _ => await _nav.PushAsync(new AddManualItemPage(_remainingCash)));

            DeleteItem = new Command<FormattedGroceryItem>(item =>
            {
                Data.Cart.Remove(Data.Cart.FirstOrDefault(i => i.Name == item.Name && Math.Abs(i.Price - item.Price) < .001 && Math.Abs(i.Quantity - item.Quantity) < .001));
                RefreshData();
            });
        }

        private bool WordContainsNonAlphaNumbers(Word w)
        {
            return !w.Text.ToCharArray().All(IsLetter);
        }

        private bool AllWordsAreNumeric(Line l)
        {
            return l.Words.All(WordIsNumeric);
        }

        private static bool WordIsNumeric(Word w)
        {
            double price;
            return double.TryParse(w.Text, out price);
        }

        private bool IsAllUpper(string input) => input.ToUpper().Equals(input);

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

        public Command DeleteItem { get; protected set; }
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
