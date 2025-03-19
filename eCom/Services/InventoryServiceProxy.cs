using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.Models;

namespace Libary.eCom.Services
{
    public class InventoryServiceProxy
    {

        public List<Product?> Products { get; private set; }


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
            Products = new List<Product?>
            {
                new Product{Id = 1, Name ="Product 1"},
                new Product{Id = 2, Name ="Product 2"},
                new Product{Id = 3, Name ="Product 3"}
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

        public Product? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }

            Product? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);

            return product;
        }

        public Product Add(Product p)
        {
            if (p.Id == 0)  // 0 means add
            {
                p.Id = LastKey + 1;
                Products.Add(p);
            }
            return p;
        }
        public Product Update(int Id, int choice, string newVal)
        {
            var product = Products.FirstOrDefault(p => p.Id == Id);
            if (choice == 1)
            {
                product.Name = newVal;
            }
            else if (choice == 2)
            {
                double price = double.Parse(newVal);
                product.Price = price;
            }
            else if (choice == 3)
            {
                int count = int.Parse(newVal);
                product.Count = count;
            }
            else if (choice == 4)
            {
                int count = int.Parse(newVal);
                product.Count += count;
            }

            return product;
        }
        
        public Product? buy(int Id, int count)
        {
            var selectedProd = Products.FirstOrDefault(p => p.Id == Id);

            if (selectedProd == null)
                return null;
            if (selectedProd.Count < count)
                return null;

            Product ShoppingCartProduct = new Product(selectedProd);

            selectedProd.Count -= count;
            ShoppingCartProduct.Count = count;


            return ShoppingCartProduct;
        }

        public Product? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

    }

    public class CartServiceProxy
    {
        public List<Product?> shoppingCart { get; private set; }

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
            shoppingCart = new List<Product?>();
        }
        

        public Product? RemoveOrDelete(int id, int count)
        {
            if (id == 0)
                return null;


            Product? product = shoppingCart.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return null;
            if (count > product.Count)
                return null;

            if (count == -1 || product.Count == count)
            {
                shoppingCart.Remove(product);
                InventoryServiceProxy.Current.Update(id, 4, product.Count.ToString());
            }
            else if (count < product.Count)
            {
                product.Count -= count;
                InventoryServiceProxy.Current.Update(id, 4, count.ToString());
            }

            return product;
        }



        public List<double> checkOut()
        {
            List<double> result = new List<double>();
            result.Add(0);
            result.Add(0);
            foreach (Product product in shoppingCart)
            {
                result[0] += product.Price * (double)product.Count;
            }
            result[1] = result[0] * 0.07;
            return result;
        }

    }
}
