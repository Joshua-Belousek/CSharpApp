using Maui.eCom.ViewModels;

namespace Maui.eCom
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingCart");
        }
    }

}
