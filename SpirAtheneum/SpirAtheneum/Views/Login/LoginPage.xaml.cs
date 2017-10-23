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
        UserVM uv;
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            uv = new UserVM(Navigation);
            BindingContext = uv;
        }
    }
}