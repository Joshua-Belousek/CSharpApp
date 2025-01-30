using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary.eCom.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string display
        {
            get
            {
                return $"{Id}. {Name} - {Count} at ${Price} / item";
            }
        }

        public override string ToString()
        {
            return display ?? string.Empty;
        }

        public Product() { 
            Name = string.Empty;
            Price = 0;
            Count = 0;
        }

        public Product(string name, double price, int count)
        {
            Name = name; Price = price; Count = count;
        }

        public Product(Product p)
        {
            Name = p.Name; Price = p.Price; Count = p.Count; Id = p.Id;
        }
    }
}
