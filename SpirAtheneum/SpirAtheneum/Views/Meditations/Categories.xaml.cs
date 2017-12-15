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
        int flag;
        //overloaded constructor
        public Categories(int? i=0)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            meditationVM = new MeditationVM();
            BindingContext = meditationVM;
            listView.ItemsSource = meditationVM.meditationList;
            Title = "All Categories";
            listViewStackLayout.Margin = new Thickness(0, -10, 0, 0);
            flag = 0;
           // show hide toolbar
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
            listViewStackLayout.Margin = new Thickness(0,10, 0, 0);
           flag = 1;
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
            //to give some space between toolbar and bottom list 
            if (flag == 1)
            {
                listViewStackLayout.Margin = new Thickness(0, 10, 0, 0);
            }
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
            //small hack to avoid space issue comming from hamberger menu
            if (flag == 1)
            {
                listViewStackLayout.Margin = new Thickness(0,-10, 0, 0);
            }
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