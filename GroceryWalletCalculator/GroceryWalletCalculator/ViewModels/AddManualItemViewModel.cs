using System;
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
    public class AddManualItemViewModel : BaseViewModel
    {
        private readonly double _remainingLimit;
        private readonly INavigation _nav;

        public AddManualItemViewModel(double remainingLimit, INavigation nav)
        {
            _remainingLimit = remainingLimit;
            _nav = nav;

            AddItemCommand = new Command(_ =>
                {
                    if (double.Parse("0" + ItemPrice)*double.Parse("0" + ItemQuantity) > _remainingLimit)
                    {
                        //TODO: let user know
                        return;
                    }
                    else
                    {
                        Data.Cart.Add(new GroceryItem
                        {
                            Name = ItemName,
                            Quantity = double.Parse("0" + ItemQuantity),
                            Price = double.Parse("0" + ItemPrice)
                        });
                        _nav.PopAsync();
                    }
                },
                _ => !string.IsNullOrWhiteSpace(ItemName) 
                    && !string.IsNullOrWhiteSpace(ItemPrice) && double.Parse("0" + ItemPrice) > 0
                    && !string.IsNullOrWhiteSpace(ItemQuantity) && double.Parse("0" + ItemQuantity) > 0);

            CancelCommand = new Command(_ => _nav.PopAsync());
        }

        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                SetProperty(ref _itemName, value);
            }
        }

        private string _itemPrice;
        public string ItemPrice
        {
            get { return _itemPrice; }
            set
            {
                SetProperty(ref _itemPrice, value);
            }
        }

        private string _itemQuantity;
        public string ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                SetProperty(ref _itemQuantity, value);
            }
        }

        public Command AddItemCommand;
        public Command CancelCommand;
    }
}
