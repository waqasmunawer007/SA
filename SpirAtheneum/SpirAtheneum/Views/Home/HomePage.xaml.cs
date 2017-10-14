using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        void DailyDigestClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.DailyDigest.DailyDigest());
        }

        void MeditationClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Meditations.Categories());
        }

        void ContentClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Content.Content());
        }

        void FavouritesClick_Handler(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Favourites.Favourites());
        }
    }
}