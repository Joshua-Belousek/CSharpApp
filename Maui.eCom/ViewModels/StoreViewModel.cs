using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.Models;
using Libary.eCom.Services;

namespace Maui.eCom.ViewModels
{
    public class StoreViewModel
    {
        public int ItemCount { get; set; }
        public int Item { get; set; }

        private InventoryServiceProxy _svc = InventoryServiceProxy.Current;
        public ObservableCollection<Product?> Inventory
        {
            get
            {
                return new ObservableCollection<Product?>(_svc.Products);
            }

        }
    }
}
