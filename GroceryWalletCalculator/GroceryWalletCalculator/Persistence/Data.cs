using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryWalletCalculator.Models;

namespace GroceryWalletCalculator.Persistence
{
    public static class Data
    {
        public static Store[] Stores = {
            new Store
            {
                Id = 1,
                Name = "Meijer",
                Image = "meijer.png"
            },
            new Store
            {
                Id = 2,
                Name = "Kroger",
                Image = "kroger.png"
            },
            new Store
            {
                Id = 3,
                Name = "Wallmart",
                Image = "wallmart.png"
            }
        };

        public static List<GroceryItem> Cart = new List<GroceryItem>();
    }
}
