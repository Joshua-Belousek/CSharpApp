using Maui.eCom.ViewModels;

namespace Maui.eCom.Views;

public partial class ShoppingCartView : ContentPage
{
	public ShoppingCartView()
	{
		InitializeComponent();
		BindingContext = new ShoppingCartViewModel();
	}

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//StoreView");
    }
}