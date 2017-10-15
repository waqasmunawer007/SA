using Services.Models.DailyDigest;
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
    public partial class DailyDigestCategoryItemDetail : ContentPage
    {
        DailyDigestModel Item;
        DailyDigestItemDetailVM dailyDigestItemDetailVM;
        public DailyDigestCategoryItemDetail(DailyDigestModel SelectedDigestItem)
        {
            InitializeComponent();
            dailyDigestItemDetailVM = new DailyDigestItemDetailVM();
            Item = SelectedDigestItem;
            BindingContext = dailyDigestItemDetailVM;

        }

        public async void FetchDigestItems()
        {
            dailyDigestItemDetailVM.IsBusy = true;
            DailyDigestModel response = await dailyDigestItemDetailVM.FetchDIgestItemDetail(Item.id);
            if (response != null)
            {
                needToShowHideLayout.IsVisible = true;

                UpdatePage(response);
                //todo
            }
            else
            {
                needToShowHideLayout.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Category list item is empty");

            }
            dailyDigestItemDetailVM.IsBusy = false;
        }
        private void UpdatePage(DailyDigestModel response)
        {
            Title = response.title;
            dailyDigestItemDetailVM.DigestItem = response;

        }
        protected override void OnAppearing()
        {
            FetchDigestItems();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            dailyDigestItemDetailVM.IsBusy = false;
            NoDataLabel.IsVisible = false;
            base.OnDisappearing();
        }
    }
}