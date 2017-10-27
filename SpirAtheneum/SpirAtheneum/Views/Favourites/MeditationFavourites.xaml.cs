﻿using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.FavouriteViewModel;
using SpirAtheneum.Views.Meditations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.Favourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeditationFavourites : ContentPage
    {
        MeditationFavouritesViewModel meditationVM;

        public MeditationFavourites()
        {
            InitializeComponent();
            meditationVM = new MeditationFavouritesViewModel();
            BindingContext = meditationVM;
            listView.ItemsSource = meditationVM.MeditationBinding;
        }

        public void FetchAllItems()
        {
            List<MeditationBinding> items = meditationVM.FetchAllFavourites();
            if (items != null && items.Count >0)
            {
                listView.IsVisible = true;
                UpdatePage(items);
            }
            else
            {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("List in MeditationFavourites page is empty");
            }
        }

        private void UpdatePage(List<MeditationBinding> data)
        {
            foreach (MeditationBinding m in data)
            {
                meditationVM.MeditationBinding.Add(m);
            }
        }

        protected override void OnAppearing()
        {
            FetchAllItems();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            meditationVM.MeditationBinding.Clear();
            listView.IsVisible = false;
            NoDataLabel.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var select = ((ListView)sender).SelectedItem;
            MeditationBinding item = (MeditationBinding)select;
            MedItemDetail medItemDetail = new MedItemDetail();
            medItemDetail.meditationItem = item;
            await Navigation.PushAsync(medItemDetail);
            ((ListView)sender).SelectedItem = null;
        }
    }
}