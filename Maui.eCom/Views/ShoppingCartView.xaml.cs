using Maui.eCom.ViewModels;

namespace Maui.eCom.Views;

public partial class ShoppingCartView : ContentPage
{
	public ShoppingCartView()
	{
		InitializeComponent();
		BindingContext = new ShoppingCartViewModel();
	}

    private void GoBack(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//StoreView");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingCartViewModel)?.update();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShoppingCartViewModel)?.RefreshProductList();
    }
}