using SpirAtheneum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

namespace SpirAtheneum.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        UserVM uv;
        public SignUp()
        {
            InitializeComponent();
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

		//public void loginButtonClicked(object sender, EventArgs e)
		//{
		//    App.Current.MainPage = new Menu.MainPage();
		//}
	}
}