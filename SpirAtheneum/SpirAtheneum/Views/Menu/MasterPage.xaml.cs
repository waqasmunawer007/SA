using SpirAtheneum.Models;
using SpirAtheneum.Views.DailyDigest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MasterPage()
        {
            InitializeComponent();

            var masterpageitems = new List<MasterPageItem>();

            masterpageitems.Add(new MasterPageItem { Title = "Home", IconSource = "icon_home_menu.png", TargetType = typeof(Home.HomePage) });
            masterpageitems.Add(new MasterPageItem { Title = "Daily Digest", IconSource = "icon_quill_menu.png", TargetType = typeof(DigestCategoryItemsPage) });
            masterpageitems.Add(new MasterPageItem { Title = "Meditations", IconSource = "icon_quill_menu.png", TargetType = typeof(Meditations.Categories) });
            masterpageitems.Add(new MasterPageItem { Title = "Content", IconSource = "icon_book_menu.png", TargetType = typeof(Content.Content) });
            masterpageitems.Add(new MasterPageItem { Title = "Favourites", IconSource = "icon_heart_menu.png", TargetType = typeof(Favourites.Favourites) });
            masterpageitems.Add(new MasterPageItem { Title = "Upgrade", IconSource = "icon_upgrade_menu.png", TargetType = typeof(Upgrade.Upgrade) });
            masterpageitems.Add(new MasterPageItem { Title = "Contact Us", IconSource = "icon_envlope_menu.png", TargetType = typeof(ContactUs.ContactUs) });
            masterpageitems.Add(new MasterPageItem { Title = "Sign Out", IconSource = "icon_signout_menu.png", TargetType = typeof(Login.LoginPage) });

            listView.ItemsSource = masterpageitems;
        }
    }
}
