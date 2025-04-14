using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.DTO;
using Libary.eCom.Models;
using Library.eCom.Util;
using Newtonsoft.Json;

namespace Libary.eCom.Services
{
    public class InventoryServiceProxy
    {

        public List<Item?> Products { get; private set; }


        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }

                return instance;
            }
        }


        private InventoryServiceProxy()
        {
            var productPayload = new WebRequestHandler().Get("/Inventory").Result;
            Products = JsonConvert.DeserializeObject<List<Item>>(productPayload) ?? new List<Item?>();
        }

        public Item? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var result = new WebRequestHandler().Delete($"/Inventory/{id}").Result;

            Item? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);
            return JsonConvert.DeserializeObject<Item>(result);
        }

        public Item Add(Item item)
        {
            var response = new WebRequestHandler().Post("/Inventory", item).Result;
            var newItem = JsonConvert.DeserializeObject<Item>(response);
            Products.Add(newItem);

            return item;
        }
        public Item? Update(Item item)
        {


            var response = new WebRequestHandler().Post("/Inventory", item).Result;
            var newItem = JsonConvert.DeserializeObject<Item>(response);

            var existingItem = Products.FirstOrDefault(p => p.Id == item.Id);
            var index = Products.IndexOf(existingItem);
            Products.RemoveAt(index);
            Products.Insert(index, new Item(newItem));

            return item;
        }

        public Item? Return(int id, int count)
        {
            var item = Products.FirstOrDefault(p => p.Id == id);

            if (item == null)
                return null;

            item.Count += count;

            return item;
        }
        
        public Item? buy(int Id, int count)
        {
            if (count == 0) return null;

            var selectedProd = Products.FirstOrDefault(p => p.Id == Id);

            if (selectedProd == null)
                return null;
            if (selectedProd.Count < count)
                return null;

            Item ShoppingCartProduct = new Item(selectedProd);

            selectedProd.Count -= count;
            ShoppingCartProduct.Count = count;


            return ShoppingCartProduct;
        }

        public Item? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

    }

    public class CartServiceProxy
    {
        public List<Item?> shoppingCart { get; private set; }

        private static CartServiceProxy? instance;
        private static object instanceLock = new object();

        public static CartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartServiceProxy();
                    }
                }

                return instance;
            }
        }

        public void add(int Id, int count)
        {
            var isInCart = shoppingCart.FirstOrDefault(p => p.Id == Id);
            var item = new Item();
            if (isInCart != null)
            {
                item = InventoryServiceProxy.Current.buy(Id, count);
                if (item != null)
                {
                    isInCart.Count += item.Count;
                }
                new WebRequestHandler().Post($"/Cart", item);
                return;
            }
            item.Id = Id;
            item.Count = count;
            new WebRequestHandler().Post($"/Cart", item);
            shoppingCart.Add(InventoryServiceProxy.Current.buy(Id, count));
        }

        public CartServiceProxy()
        {
            shoppingCart = new List<Item?>();
        }
        

        public Item? RemoveOrDelete(int id, int count)
        {
            if (id == 0)
                return null;


            Item? item = shoppingCart.FirstOrDefault(p => p?.Id == id);
            if (item == null)
                return null;
            if (count > item.Count)
                return null;

            new WebRequestHandler().Delete($"/Cart/{id}/{count}");

            if (count == -1 || item.Count == count)
            {
                shoppingCart.Remove(item);
                InventoryServiceProxy.Current.Return(id,item.Count);
            }
            else if (count < item.Count)
            {
                item.Count -= count;
                InventoryServiceProxy.Current.Return(id,count);
            }

            return item;
        }



        public List<double> checkOut()
        {
            List<double> result = new List<double>();
            result.Add(0);
            result.Add(0);
            foreach (Item item in shoppingCart)
            {
                result[0] += item.Product.Price * (double)item.Count;
            }
            result[1] = result[0] * 0.07;
            return result;
        }

        public void ClearCart()
        {
            shoppingCart.Clear();
        }

    }
}
