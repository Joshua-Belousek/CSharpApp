using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.eCom.Models
{
    public class Item
    {
        public int Count { get; set; }

        public int Id { get; set; }
        public Product? Product { get; set; }

        public override string ToString()
        {
            return $"{Product} Quantity:{Count}";
        }

        public string display
        {
            get
            {
                return Product?.display + $" Quantity:{Count}" ?? string.Empty;
            }
        }

        public Item()
        {
            Product = new Product();
        }

        public Item(Item item)
        {
            Product = new Product(item.Product);
            Count = item.Count;
            Id = item.Id;
        }
    }
}
