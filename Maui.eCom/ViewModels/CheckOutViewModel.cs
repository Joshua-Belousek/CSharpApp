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
    public class CheckOutViewModel : INotifyPropertyChanged
    {
        private List<Double> Totals { 
            get
            {
                return cart.checkOut();
            }
        }

        public string subTotal
        {
            get
            {
                return "SubTotal - " + Totals[0].ToString();
            }
        }
        public string Total
        {
            get
            {
                return "Total - " + (Totals[1] + Totals[0]).ToString();
            }
        }

        public ObservableCollection<Item?> Inventory
        {
            get
            {
                return new ObservableCollection<Item?>(cart.shoppingCart);
            }

        }

        private CartServiceProxy cart = CartServiceProxy.Current;

        public void Checkout ()
        {
            cart.ClearCart();
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
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(subTotal));
            NotifyPropertyChanged(nameof(Total));
        }
    }
}
