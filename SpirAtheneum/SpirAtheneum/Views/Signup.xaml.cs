using SpirAtheneum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        UserViewModel uv;
        public Signup()
        {
            InitializeComponent();
            uv = new UserViewModel();
            BindingContext = uv;

        }
        public void loginButtonClicked(object sender, EventArgs e)
        {


            App.Current.MainPage = new Menu.MainPage();
        }
    }
}