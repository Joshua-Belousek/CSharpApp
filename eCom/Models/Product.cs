﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.DTO;
using Libary.eCom.Models;

namespace Libary.eCom.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public double Price { get; set; }

        public string display
        {
            get
            {
                return $"{Id}. {Name} - ${Price} / item";
            }
        }

        public override string ToString()
        {
            return display ?? string.Empty;
        }

        public Product() { 
            Name = string.Empty;
            Price = 0;
        }

        public Product(string name, double price)
        {
            Name = name; Price = price;
        }


        public Product(Product p)
        {
            Name = p.Name; Price = p.Price; Id = p.Id;
        }
        public Product(ProductDTO p)
        {
            Name = p.Name; Price = p.Price; Id = p.Id;
        }
    }
}
