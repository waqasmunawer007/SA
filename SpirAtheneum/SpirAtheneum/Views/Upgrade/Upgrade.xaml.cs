using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models.Subscription;
using SpirAtheneum.Helpers;
using SpirAtheneum.ViewModels.Subscription;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.Upgrade
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Upgrade : ContentPage
	{
        SubscriptionViewModel viewModel;
		public Upgrade ()
		{
			InitializeComponent ();
            viewModel = new SubscriptionViewModel();
            BindingContext = viewModel;
		}
        protected async override void OnAppearing()
		{
			base.OnAppearing();
			if (Settings.IsSubscriped)
			{
				ADMob.IsVisible = false;
			}
			else
			{
				ADMob.IsVisible = true;
			}
            await viewModel.FetchSubscriptionItems();
		}
	}
}