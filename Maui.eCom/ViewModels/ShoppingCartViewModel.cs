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
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        public Item? Item { get; set; }
        public int ItemCount { get; set; }

        public string? Query { get; set; }

        private CartServiceProxy cart = CartServiceProxy.Current;

        public ObservableCollection<Item?> Inventory
        {
            get
            {
                var filteredList = cart.shoppingCart.Where(p => p?.Product.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                return new ObservableCollection<Item?>(filteredList);
            }

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

        public void update()
        {
            if (Item is null)
                return;
            cart.RemoveOrDelete(Item.Id, ItemCount);
            NotifyPropertyChanged(nameof(Inventory));
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Inventory));
        }
    }
}
