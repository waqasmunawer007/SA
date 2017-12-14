using SpirAtheneum.ViewModels.DailyDigestViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

namespace SpirAtheneum.Views.DailyDigest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyDigestItemsPage : ContentPage
    {
        DailyDigestItemsVM dailyDigestItemsVM;

       public DailyDigestItemsPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            dailyDigestItemsVM = new DailyDigestItemsVM();
            BindingContext = dailyDigestItemsVM;
            listView.ItemsSource = dailyDigestItemsVM.dailyDigestItems;
            toolbar.IsVisible = false;

        }
        public DailyDigestItemsPage(int? i = 0)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            dailyDigestItemsVM = new DailyDigestItemsVM();
            BindingContext = dailyDigestItemsVM;
            listView.ItemsSource = dailyDigestItemsVM.dailyDigestItems;
            if (i == 1)
                toolbar.IsVisible = true;
            else
                toolbar.IsVisible = false;

        }

        public async void FetchAllDigest()
        {
            dailyDigestItemsVM.IsBusy = true;
            List<Models.DailyDigest> items = await dailyDigestItemsVM.DatabaseOperation();
            if (items != null && items.Count > 0)
            {
                listView.IsVisible = true;
                UpdatePage(items);
            }
            else
            {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Digest Category list item is empty");
            }
            dailyDigestItemsVM.IsBusy = false;

        }

        private void UpdatePage(List<Models.DailyDigest> data)
        {
            foreach (Models.DailyDigest m in data)
            {
                dailyDigestItemsVM.dailyDigestItems.Add(m);
            }
        }

        protected override void OnAppearing()
        {
			if (Settings.IsSubscriped)
			{
				ADMob.IsVisible = false;
			}
			else
			{
				ADMob.IsVisible = true;
			}
            FetchAllDigest();

            base.OnAppearing();

        }
        protected override void OnDisappearing()
        {
            dailyDigestItemsVM.dailyDigestItems.Clear();
            NoDataLabel.IsVisible = false;
            dailyDigestItemsVM.IsBusy = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }

        private void  listView_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedCategory = ((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;
        }
        private void BackTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}