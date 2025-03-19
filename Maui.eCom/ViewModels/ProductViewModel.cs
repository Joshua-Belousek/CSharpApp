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
                return Model?.Product?.Name ?? string.Empty;
            }

            set
            {
                if (Model != null && Model?.Product?.Name != value)
                {
                    Model.Product.Name = value ?? "";
                }
            }
        }
         
        public double? Price
        {
            get
            {
                return Model?.Product.Price ?? 0;
            }

            set
            {
                if (Model != null && Model.Product.Price != value)
                {
                    Model.Product.Price = value ?? -1;
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

        public Item? Model { get; set; }


        public ProductViewModel()
        {
            Model = new Item();
        }

        public ProductViewModel(Item? model)
        {
            Model = new Item(model);
        }

        public void add()
        {
            if (Model.Id == 0)
            {
                InventoryServiceProxy.Current.Add(Model);
                return;
            }


            InventoryServiceProxy.Current.Update(Model.Id,1,Model.Product.Name);
            InventoryServiceProxy.Current.Update(Model.Id, 2, Model.Product.Price.ToString());
            InventoryServiceProxy.Current.Update(Model.Id, 3, Model.Count.ToString());
        }
         
    }
}
