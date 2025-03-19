using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.Models;
using Libary.eCom.Services;

namespace Maui.eCom.ViewModels
{
    public class ProductViewModel
    {
        public string? Name
        {
            get
            {
                return Model?.Name ?? string.Empty;
            }

            set
            {
                if (Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }

        public double? Price
        {
            get
            {
                return Model?.Price ?? 0;
            }

            set
            {
                if (Model != null && Model.Price != value)
                {
                    Model.Price = value ?? -1;
                }
            }
        }

        public int? Count
        {
            get
            {
                return Model?.Count ?? 0;
            }

            set
            {
                if (Model != null && Model.Count != value)
                {
                    Model.Count = value ?? 0;
                }
            }
        }

        public Product? Model { get; set; }


        public ProductViewModel()
        {
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            Model = new Product(model);
        }

        public void add()
        {
            InventoryServiceProxy.Current.Update(Model.Id,1,Model.Name);
            InventoryServiceProxy.Current.Update(Model.Id, 2, Model.Price.ToString());
            InventoryServiceProxy.Current.Update(Model.Id, 3, Model.Count.ToString());
        }

    }
}
