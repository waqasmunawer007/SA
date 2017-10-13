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
           




        }

        void Button1Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.DailyDigest.DailyDigest());
        }

        void Button2Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Meditations.Categories());
        }

        void Button3Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Content.Content());
        }

        void Button4Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Favourites.Favourites());
        }
    }
}