using System;
using System.Collections.Generic;
using SpirAtheneum.Helpers;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.Account
{
    public partial class ChangePasswordPage : ContentPage
    {
        ChagePasswordViewModel viewModel;
        public ChangePasswordPage()
        {
            InitializeComponent();
            viewModel = new ChagePasswordViewModel(this.Navigation);
            BindingContext = viewModel;
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
        private void BackTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
