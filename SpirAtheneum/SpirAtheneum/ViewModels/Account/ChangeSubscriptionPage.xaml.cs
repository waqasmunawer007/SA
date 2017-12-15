using System;
using System.Collections.Generic;
using SpirAtheneum.Helpers;
using SpirAtheneum.ViewModels.Subscription;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.Account
{
    public partial class ChangeSubscriptionPage : ContentPage
    {
        SubscriptionViewModel viewModel;
        public ChangeSubscriptionPage()
        {
            InitializeComponent();
            viewModel = new SubscriptionViewModel();
            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
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
            await viewModel.FetchSubscriptionItems();
        }
        private void BackTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
