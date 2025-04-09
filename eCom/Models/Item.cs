using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.DTO;

namespace Libary.eCom.Models
{
    public class Item
    {
        public int Count { get; set; }

        public int Id { get; set; }
        public ProductDTO? Product { get; set; }

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
            Product = new ProductDTO();
        }

        public Item(Item item)
        {
            Product = new ProductDTO(item.Product);
            Count = item.Count;
            Id = item.Id;
        }
    }
}
