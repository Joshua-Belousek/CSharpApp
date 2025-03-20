using Maui.eCom.ViewModels;

namespace Maui.eCom.Views;

public partial class CheckOutView : ContentPage
{
	public CheckOutView()
	{
		InitializeComponent();
		BindingContext = new CheckOutViewModel();
	}

    private void Checkout(object sender, EventArgs e)
    {
		(BindingContext as CheckOutViewModel).Checkout();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShoppingCart");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CheckOutViewModel)?.RefreshProductList();
    }
}