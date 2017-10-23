using Services.Models.Meditation;
using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.Meditations.CategoryItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryItems : ContentPage
    {
        MeditationItemsVM meditationVM;

        string selectCategory;
        public CategoryItems(string category)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            meditationVM = new MeditationItemsVM(category);
            BindingContext = meditationVM;
            listView.ItemsSource = meditationVM.meditationList;
            selectCategory = category;
            Title = category;
        }

       public void FetchAllItems()
       {
            List<Meditation> categoryItems = meditationVM.FetchAllCategoryItems();
            if (categoryItems != null && categoryItems.Count > 0)
             {
                    listView.IsVisible = true;
                    UpdatePage(categoryItems);
             }
            else
             {
                    listView.IsVisible = false;
                    Debug.WriteLine("Category list item is empty");
             }
        }

        private void UpdatePage(List<Meditation> data)
        {
            foreach (Meditation m in data)
            {
                meditationVM.meditationList.Add(m);
            }
        }

        protected override void OnAppearing()
        {
            FetchAllItems();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            meditationVM.meditationList.Clear();
            listView.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            Meditation item = (Meditation)selectedCategory;
            await Navigation.PushAsync(new MedItemDetail(item));
            ((ListView)sender).SelectedItem = null;

        }
    }
}