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

        public string display
        {
            get
            {
                return $"{Id}. {Name}";
            }
        }

        public override string ToString()
        {
            return display ?? string.Empty;
        }

        public Product() { Name = string.Empty; }
    }
}
