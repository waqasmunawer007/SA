using System;
using System.Collections.Generic;
using SpirAtheneum.Helpers;
using SpirAtheneum.ViewModels.Account;
using Xamarin.Forms;

namespace SpirAtheneum.Views.Account
{
    public partial class AccountPage : ContentPage
    {
        AccountViewModel viewModel;
        public AccountPage()
        {
            InitializeComponent();
            viewModel = new AccountViewModel(Navigation);
            BindingContext = viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Settings.IsSubscriped)
            {
                ADMob.IsVisible = false;
                SubscriptionButton.IsEnabled = true;

            }
            else
            {
                ADMob.IsVisible = true;
                SubscriptionButton.IsEnabled = false;
            }


        }
    }
}
