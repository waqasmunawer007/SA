using SpirAtheneum.Views.DailyDigest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;
using Plugin.FirebasePushNotification;
using Plugin.LocalNotifications;

namespace SpirAtheneum.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");

        }

		protected override void OnAppearing()
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

		}

        void DailyDigestClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.DailyDigest.DailyDigestItemsPage());
        }

        void MeditationClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Meditations.Categories());
        }

        void KnowledgeBaseClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.KnowledgeBase.Categories());
        }

        //void ContentClick_Handler(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new Views.Content.Content());
        //}

        void FavouritesClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Favourites.Favourites());
        }

    }
}