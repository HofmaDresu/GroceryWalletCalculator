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
        public static string foo = "asdf";
        public static Store[] Stores = {
            new Store
            {
                Id = 1,
                Name = "Meijer",
                Image = "meijer"
            },
            new Store
            {
                Id = 2,
                Name = "Kroger",
                Image = "kroger"
            },
            new Store
            {
                Id = 3,
                Name = "Wallmart",
                Image = "wallmart"
            }
        };
    }
}
