using System.ComponentModel;
using System.Runtime.CompilerServices;
using Maui.eCom.ViewModels;

namespace Maui.eCom.Views;

public partial class StoreView : ContentPage
{
	public StoreView()
	{
		InitializeComponent();
		BindingContext = new StoreViewModel();
	}
    private void GoBack(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

	private void BuyClicked(Object sender, EventArgs e)
	{
		(BindingContext as StoreViewModel)?.buy();
		
	}

}