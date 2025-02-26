using Maui.eCom.ViewModels;

namespace Maui.eCom.Views;

    public partial class ProductDetails : ContentPage
    {
        public ProductDetails()
        {
            InitializeComponent();
            BindingContext = new ProductViewModel();
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }
    }