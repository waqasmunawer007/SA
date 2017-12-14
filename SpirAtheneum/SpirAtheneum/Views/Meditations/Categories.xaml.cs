using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

namespace SpirAtheneum.Views.Meditations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categories : ContentPage
    {
        MeditationVM meditationVM;

        //overloaded constructor
        public Categories(int? i=0)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            meditationVM = new MeditationVM();
            BindingContext = meditationVM;
            listView.ItemsSource = meditationVM.meditationList;
            Title = "All Categories";
            //show hide toolbar
            if (i == 1)
                toolbar.IsVisible = true;
            else
                toolbar.IsVisible = false;
        }
        //default constructor
        public Categories()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            meditationVM = new MeditationVM();
            BindingContext = meditationVM;
            listView.ItemsSource = meditationVM.meditationList;
            Title = "All Categories";
            toolbar.IsVisible = false;

        }

        public async void FetchAllMeditationAsync()
        {
            meditationVM.IsBusy = true;
            List<Category> meditationCategories = await meditationVM.DatabaseOperation();

            if (meditationCategories != null && meditationCategories.Count > 0)
            {
                listView.IsVisible = true;
                UpdatePage(meditationCategories);
            }
            else
            {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Meditation category list is empty");
            }

            meditationVM.IsBusy = false;
        }

        private void UpdatePage(List<Category> data)
        {
            foreach (Category k in data)
            {
                meditationVM.meditationList.Add(k);
            }
        }
        protected override void OnAppearing()
        {
            FetchAllMeditationAsync();
            if (Settings.IsSubscriped)
            {
                ADMob.IsVisible = false;
            }
            else
            {
                ADMob.IsVisible = true;
            }
            base.OnAppearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            Category category = (Category)selectedCategory;
            await Navigation.PushModalAsync(new CategoryItems.CategoryItems(category.category));
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnDisappearing()
        {
            meditationVM.meditationList.Clear();
            NoDataLabel.IsVisible = false;
            meditationVM.IsBusy = false;
            listView.IsVisible = false;
            ADMob.IsVisible = false;
            base.OnDisappearing();
        }

        private void BackTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}