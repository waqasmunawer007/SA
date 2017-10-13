using SpirAtheneum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserViewModel uv;
        public LoginPage()
        {
            InitializeComponent();
            uv = new UserViewModel();
            BindingContext = uv;
        }

        //public void loginButtonClicked(object sender, EventArgs e)
        //{
        //    App.Current.MainPage = new Menu.MainPage();
            
        //}
    }
}