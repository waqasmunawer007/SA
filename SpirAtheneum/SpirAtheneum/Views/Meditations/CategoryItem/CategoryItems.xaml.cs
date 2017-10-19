using Services.Models.Meditation;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            listView.ItemsSource = meditationVM.meditaions;
            selectCategory = category;
            Title = category;
        }

       public async void FetchAllMeditationsCategoryItems()
       {
            meditationVM.IsBusy = true;
            List<MeditationModel> categoryItems = await meditationVM.FetchAllMeditationCategoryItems();
            if (categoryItems != null && categoryItems.Count > 0)
             {
                    listView.IsVisible = true;
                    UpdatePage(categoryItems);
             }
            else
             {
                    listView.IsVisible = false;
                    NoDataLabel.IsVisible = true;
                    Debug.WriteLine("Category list item is empty");
             }
            meditationVM.IsBusy = false;
            
        }
        private void UpdatePage(List<MeditationModel> data)
        {
            foreach (MeditationModel m in data)
            {
                meditationVM.meditaions.Add(m);
            }
        }

        protected override void OnAppearing()
        {
            FetchAllMeditationsCategoryItems();

            base.OnAppearing();
           
        }
        protected override void OnDisappearing()
        {
            meditationVM.meditaions.Clear();
            NoDataLabel.IsVisible = false;
            meditationVM.IsBusy = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            MeditationModel item = (MeditationModel)selectedCategory;
            await Navigation.PushAsync(new MedItemDetail(item));
            ((ListView)sender).SelectedItem = null;

        }
    }
}