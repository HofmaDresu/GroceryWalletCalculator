using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GroceryWalletCalculator.Persistence;
using MvvmHelpers;
using Xamarin.Forms;

namespace GroceryWalletCalculator.ViewModels
{
    public class SetSpendingLimitViewModel : BaseViewModel
    {
        private int _storeId;
        public SetSpendingLimitViewModel(int storeId)
        {
            _storeId = storeId;
            SpendingText = $"How much can you spend at {Data.Stores.Single(s => s.Id == storeId).Name}?";
            StartShoppingCommand = new Command(_ => { }, _ => !string.IsNullOrWhiteSpace(SpendingLimit) && double.Parse("0"+SpendingLimit) > 0);
        }

        private string _spendingLimit;
        public string SpendingLimit
        {
            get { return _spendingLimit; }
            set
            {
                SetProperty(ref _spendingLimit, value);
                StartShoppingCommand.ChangeCanExecute();
            }
        }

        private string _spendingText;
        public string SpendingText
        {
            get { return _spendingText; }
            set { SetProperty(ref _spendingText, value); }
        }

        public Command StartShoppingCommand { get; protected set; }
    }
}
