using SpirAtheneum.Helpers;
using SpirAtheneum.Models;
using SpirAtheneum.Views.Login;
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
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                if (item.TargetType == typeof(Login.LoginPage))
                {

                    Settings.Login = 0;
                    App.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {

                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    MasterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }
    }
}