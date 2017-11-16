using SpirAtheneum.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

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
        //public void LoginClicked(Object sender, EventArgs e)
        //{
        //    var email = Email_Entry.ToString();
        //    var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

        //    if(Regex.IsMatch(email, emailPattern))
        //    {
        //        emailvalidation.IsEnabled = false;
        //        emailvalidation.IsVisible = false;
        //    }
        //    else
        //    {
        //        emailvalidation.IsEnabled = false;
        //        emailvalidation.IsVisible = false;
        //    }
        //}
    }
}