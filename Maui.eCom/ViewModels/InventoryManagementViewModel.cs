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
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public Item? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private InventoryServiceProxy _svc = InventoryServiceProxy.Current;
        public ObservableCollection<Item?> Items
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Product.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
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

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Items));
        }

        public Item? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Items");
            return item;
        }
    }
}
