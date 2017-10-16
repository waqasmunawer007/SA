using SpirAtheneum.ViewModels.DailyDigestViewModel;
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

namespace SpirAtheneum.Views.DailyDigest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DigestCategoryPage : ContentPage
    {
        DigestCategoryViewModel digestCategoryVM;
        public DigestCategoryPage()
        {
            InitializeComponent();
            digestCategoryVM = new DigestCategoryViewModel();
            BindingContext = digestCategoryVM;
            listView.ItemsSource = digestCategoryVM.DigestCategories;
            Title = "All Categories";
        }
        public async void FetchAllDigestCategoryAsync()
        {
            digestCategoryVM.IsBusy = true;
            List<MeditationVM.Category> digestCategories= await digestCategoryVM.FetchAllCategoryCategory();

            if (digestCategories != null && digestCategories.Count > 0)
            {
                listView.IsVisible = true;
                UpdatePage(digestCategories);
            }
            else
            {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Digest category list is empty");

            }
            digestCategoryVM.IsBusy = false;
        }
        private void UpdatePage(List<Category> data)
        {
            foreach (Category m in data)
            {
                digestCategoryVM.DigestCategories.Add(m);
            }
        }
        protected override void OnAppearing()
        {
            FetchAllDigestCategoryAsync();
           base.OnAppearing();

        }
        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           
            var selectedCategory = ((ListView)sender).SelectedItem;
            Category category = (Category)selectedCategory;
            await Navigation.PushAsync(new DigestCategoryItemsPage());
           
            ((ListView)sender).SelectedItem = null;

        }
        protected override void OnDisappearing()
        {
            digestCategoryVM.DigestCategories.Clear();
            NoDataLabel.IsVisible = false;
            digestCategoryVM.IsBusy = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }

    }
}