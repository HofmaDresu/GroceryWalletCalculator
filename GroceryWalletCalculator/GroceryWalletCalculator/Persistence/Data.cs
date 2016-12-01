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

        public static List<GroceryItem> Cart = new List<GroceryItem>
        {
            new GroceryItem
            {
                Name = "Eggs",
                Price = 6.99,
                Quantity = 2
            },
            new GroceryItem
            {
                Name = "Bacon",
                Price = 10.99,
                Quantity = 1
            },
            new GroceryItem
            {
                Name = "Milk",
                Price = 2.99,
                Quantity = 1
            }
        };
    }
}
