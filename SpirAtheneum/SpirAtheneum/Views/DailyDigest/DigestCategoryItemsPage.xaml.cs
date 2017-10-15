using Services.Models.DailyDigest;
using Services.Models.Meditation;
using SpirAtheneum.ViewModels.DailyDigestViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.DailyDigest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DigestCategoryItemsPage : ContentPage
    {
        DailyDigestCategoryItemsVM dailyDigestCategoryItemsVM;

        string selectCategory;
        public DigestCategoryItemsPage(string category)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            dailyDigestCategoryItemsVM = new DailyDigestCategoryItemsVM(category);
            BindingContext = dailyDigestCategoryItemsVM;
            listView.ItemsSource = dailyDigestCategoryItemsVM.digetCategotyItems;
            selectCategory = category;
            Title = category;
        }

        public async void FetchAllDigestCategoryItems()
        {
            dailyDigestCategoryItemsVM.IsBusy = true;
            List<DailyDigestModel> categoryItems = await dailyDigestCategoryItemsVM.FetchAllDigestCategoryItems();
            if (categoryItems != null && categoryItems.Count > 0)
            {
                listView.IsVisible = true;
                UpdatePage(categoryItems);
            }
            else
            {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Digest Category list item is empty");
            }
            dailyDigestCategoryItemsVM.IsBusy = false;

        }

        private void UpdatePage(List<DailyDigestModel> data)
        {
            foreach (DailyDigestModel m in data)
            {
                dailyDigestCategoryItemsVM.digetCategotyItems.Add(m);
            }
        }

        protected override void OnAppearing()
        {
            FetchAllDigestCategoryItems();

            base.OnAppearing();

        }
        protected override void OnDisappearing()
        {
            dailyDigestCategoryItemsVM.digetCategotyItems.Clear();
            NoDataLabel.IsVisible = false;
            dailyDigestCategoryItemsVM.IsBusy = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }

        private async Task listView_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            DailyDigestModel item = (DailyDigestModel)selectedCategory;
            await Navigation.PushAsync(new DailyDigestCategoryItemDetail(item));
            ((ListView)sender).SelectedItem = null;

        }
    }
}