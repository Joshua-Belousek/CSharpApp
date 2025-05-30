﻿using System;
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
        public Item? Item { get; set; }

        private InventoryServiceProxy _svc = InventoryServiceProxy.Current;

        private CartServiceProxy cart = CartServiceProxy.Current;
        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(_svc.Products);
            }

        }

        public void buy()
        {
            if (Item == null)
                return;
            cart.add(Item.Id,ItemCount);
            NotifyPropertyChanged("Inventory");
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Inventory));
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
