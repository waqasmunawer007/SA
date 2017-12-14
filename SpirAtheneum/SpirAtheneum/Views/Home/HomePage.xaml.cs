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
using SpirAtheneum.Views.Login;

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
            Navigation.PushModalAsync(new Views.DailyDigest.DailyDigestItemsPage(1));
        }

        void MeditationClick_Handler(object sender, EventArgs e)
        {
            /// <summary>
            /// want to call overloaded constructor; purpose is to show the customize toolbar
            /// </summary>
            Navigation.PushModalAsync(new Views.Meditations.Categories(1),false);
        }
       
        void KnowledgeBaseClick_Handler(object sender, EventArgs e)
        {
            /// <summary>
            /// want to call overloaded constructor; purpose is to show the customize toolbar
            /// </summary>
            Navigation.PushModalAsync(new Views.KnowledgeBase.Categories(1));
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