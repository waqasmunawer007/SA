using Services.Models.Meditation;
using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SpirAtheneum.ViewModels.MeditationViewModel.MeditationVM;

namespace SpirAtheneum.Views.Meditations
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Categories : ContentPage
	{
        MeditationVM meditationVM;
		public Categories ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            meditationVM = new MeditationVM();
            BindingContext = meditationVM;
            listView.ItemsSource = meditationVM.meditaions;
            Title = "All Categories";

        }
       public async void FetchAllMeditationsCategoryAsync()
        {
            meditationVM.IsBusy = true;
            List<Category> meditations = await meditationVM.FetchAllMeditationCategory();
            
            if (meditations != null && meditations.Count > 0)
            {
                listView.IsVisible = true;
                UpdatePage(meditations);
            }
            else {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Meditation category list is empty");

            }
            meditationVM.IsBusy = false;
        }

      

        private void UpdatePage(List<Category> data)
        {
            foreach(Category m in data)
            {
                meditationVM.meditaions.Add(m);
            }
        }
        protected override void OnAppearing()
        {
            FetchAllMeditationsCategoryAsync();
            base.OnAppearing();
           
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            Category category = (Category)selectedCategory;
            await Navigation.PushAsync(new CategoryItems.CategoryItems(category.category));
            ((ListView)sender).SelectedItem = null;

        }
        protected override void OnDisappearing()
        {
            meditationVM.meditaions.Clear();
            NoDataLabel.IsVisible = false;
            meditationVM.IsBusy = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }
    }
}