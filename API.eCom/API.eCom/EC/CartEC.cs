﻿using API.eCom.Database;
using Libary.eCom.Models;
using Libary.eCom.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;

namespace API.eCom.EC
{
    public class CartEC
    {

        public List<Item?> Get()
        {
            return FakeDatabase.Cart;
        }


        public void add(int id, int count)
        {
            var isInCart = FakeDatabase.Cart.FirstOrDefault(i => i?.Id == id);
            if (isInCart == null)
            {
                FakeDatabase.Cart.Add(new InventoryEC().buy(id, count));
                return;
            }

            var item = new InventoryEC().buy(id, count);
            isInCart.Count += item.Count;

        }

        public Item? RemoveOrDelete(int id, int count)
        {
            if (id == 0)
                return null;


            Item? item = FakeDatabase.Cart.FirstOrDefault(p => p?.Id == id);
            if (item == null)
                return null;
            if (count > item.Count)
                return null;

            if (count == -1 || item.Count == count)
            {
                FakeDatabase.Cart.Remove(item);
                new InventoryEC().Return(id, item.Count);
            }
            else if (count < item.Count)
            {
                item.Count -= count;
                new InventoryEC().Return(id, count);
            }

            return item;
        }

        public List<double> checkOut()
        {
            List<double> result = new List<double>();
            result.Add(0);
            result.Add(0);
            foreach (Item item in FakeDatabase.Cart)
            {
                result[0] += item.Product.Price * (double)item.Count;
            }
            result[1] = result[0] * 0.07;
            return result;
        }

        public void ClearCart()
        {
            FakeDatabase.Prices.Add(checkOut());
            FakeDatabase.Purchased.Add(FakeDatabase.Cart.ConvertAll(p => new Item(p)));
            FakeDatabase.Cart.Clear();
        }





    }
}
