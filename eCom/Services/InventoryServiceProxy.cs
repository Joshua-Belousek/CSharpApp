using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.DTO;
using Libary.eCom.Models;

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
            Products = new List<Item?>
            {
                new Item{Product = new ProductDTO{Id = 1, Name ="Product 1"}, Id = 1, Count = 1 },
                new Item{Product = new ProductDTO{Id = 2, Name ="Product 2"}, Id = 2, Count = 2 },
                new Item { Product = new ProductDTO{ Id = 3, Name = "Product 3" }, Id = 3, Count = 3 }
            };
        }
        private int LastKey
        {
            get
            {
                if (!Products.Any())
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        public Item? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }

            Item? Item = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(Item);

            return Item;
        }

        public Item Add(Item p)
        {
            if (p.Id == 0)  // 0 means add
            {
                p.Id = LastKey + 1;
                p.Product.Id = p.Id;
                Products.Add(p);
            }
            return p;
        }
        public Item Update(int Id, int choice, string newVal)
        {
            var item = Products.FirstOrDefault(p => p.Id == Id);
            if (choice == 1)
            {
                item.Product.Name = newVal;
            }
            else if (choice == 2)
            {
                double price = double.Parse(newVal);
                item.Product.Price = price;
            }
            else if (choice == 3)
            {
                int count = int.Parse(newVal);
                item.Count = count;
            }
            else if (choice == 4)
            {
                int count = int.Parse(newVal);
                item.Count += count;
            }

            return item;
        }
        
        public Item? buy(int Id, int count)
        {
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
            if (isInCart != null)
            {
                var item = InventoryServiceProxy.Current.buy(Id, count);
                if (item != null)
                {
                    isInCart.Count += item.Count;
                }
                return;
            }
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

            if (count == -1 || item.Count == count)
            {
                shoppingCart.Remove(item);
                InventoryServiceProxy.Current.Update(id, 4, item.Count.ToString());
            }
            else if (count < item.Count)
            {
                item.Count -= count;
                InventoryServiceProxy.Current.Update(id, 4, count.ToString());
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
