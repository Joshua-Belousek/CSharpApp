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
                return cachedModel?.Product?.Name ?? string.Empty;
            }

            set
            {
                if (Model != null && Model?.Product?.Name != value)
                {
                    cachedModel.Product.Name = value ?? "";
                }
            }
        }

         
        public double? Price
        {
            get
            {
                return cachedModel?.Product.Price ?? 0;
            }

            set
            {
                if (cachedModel != null && cachedModel.Product?.Price != value)
                {
                    cachedModel.Product.Price = value ?? -1;
                }
            }
        }

        public int? Count
        {
            get
            {
                return cachedModel?.Count ?? 0;
            }

            set
            {
                if (cachedModel != null && cachedModel.Count != value)
                {
                    cachedModel.Count = value ?? 0;
                }
            }
        }

        public Item? Model { get; set; }

        private Item? cachedModel { get; set; }


        public ProductViewModel()
        {
            cachedModel = new Item();
        }

        public ProductViewModel(Item? model)
        {
            cachedModel = new Item(model);
        }

        public void add()
        {
            if (cachedModel.Id == 0)
            {
                InventoryServiceProxy.Current.Add(cachedModel);
                return;
            }


            InventoryServiceProxy.Current.Update(cachedModel.Id,1, cachedModel.Product.Name);
            InventoryServiceProxy.Current.Update(cachedModel.Id, 2, cachedModel.Product.Price.ToString());
            InventoryServiceProxy.Current.Update(cachedModel.Id, 3, cachedModel.Count.ToString());
        }
         
    }
}
