﻿using Libary.eCom.DTO;
using Libary.eCom.Models;

namespace API.eCom.Database
{
    public static class FakeDatabase
    {
        private static List<Item?> inventory = new List<Item?>
         {
             new Item{ Product = new ProductDTO{Id = 1, Name ="Product 1 WEB"}, Id = 1, Count = 1 },
             new Item{ Product = new ProductDTO{Id = 2, Name ="Product 2 WEB"}, Id = 2 , Count = 2 },
             new Item{ Product = new ProductDTO{Id = 3, Name ="Product 3 WEB"}, Id=3 , Count = 3 }
         };

        public static int LastKey_Item
        {
            get
            {
                if (!inventory.Any())
                {
                    return 0;
                }

                return inventory.Select(p => p?.Id ?? 0).Max();
            }
        }
        public static List<Item?> Inventory
        {
            get
            {
                return inventory;
            }
        }

        private static List<Item?> cart = new List<Item?>();

        public static List<Item?> Cart
        {
            get
            {
                return cart;
            }
        }

        private static List<List<Item?>> purchased = new List<List<Item?>>();

        public static List<List<Item?>> Purchased
        {
            get
            {
                return purchased;
            }
        }

        private static List<List<Double>> prices = new List<List<Double>>(); 

        public static List <List<Double>> Prices
        {
            get
            {
                return prices;
            }
        }


    }
}
