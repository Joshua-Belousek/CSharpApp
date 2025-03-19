using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.Models;
using Libary.eCom.Services;

namespace Maui.eCom.ViewModels
{
    public class StoreViewModel : INotifyPropertyChanged
    {
        public int ItemCount { get; set; }
        public Product? Item { get; set; }

        private InventoryServiceProxy _svc = InventoryServiceProxy.Current;

        private CartServiceProxy cart = CartServiceProxy.Current;
        public ObservableCollection<Product?> Inventory
        {
            get
            {
                return new ObservableCollection<Product?>(_svc.Products);
            }

        }

        public void buy()
        {
            cart.add(Item.Id,ItemCount);
            NotifyPropertyChanged("Inventory");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
