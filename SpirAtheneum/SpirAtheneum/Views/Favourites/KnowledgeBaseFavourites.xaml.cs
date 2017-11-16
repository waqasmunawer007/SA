using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.FavouriteViewModel;
using SpirAtheneum.Views.KnowledgeBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

namespace SpirAtheneum.Views.Favourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KnowledgeBaseFavourites : ContentPage
    {
        KnowledgeBaseFavouritesViewModel knowledgeVM;

        public KnowledgeBaseFavourites()
        {
            InitializeComponent();
            knowledgeVM = new KnowledgeBaseFavouritesViewModel();
            BindingContext = knowledgeVM;
            listView.ItemsSource = knowledgeVM.KnowledgeBaseBinding;
        }

        public void FetchAllItems()
        {
            List<KnowledgeBaseBinding> items = knowledgeVM.FetchAllFavourites();
            if (items != null && items.Count > 0)
            {
                listView.IsVisible = true;
                UpdatePage(items);
            }
            else
            {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("List in KnowledgeBaseFavourites page is empty");
            }
        }

        private void UpdatePage(List<KnowledgeBaseBinding> data)
        {
            foreach (KnowledgeBaseBinding k in data)
            {
                knowledgeVM.KnowledgeBaseBinding.Add(k);
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
            FetchAllItems();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            knowledgeVM.KnowledgeBaseBinding.Clear();
            listView.IsVisible = false;
            NoDataLabel.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var select = ((ListView)sender).SelectedItem;
            KnowledgeBaseBinding item = (KnowledgeBaseBinding)select;
            KnowledgeBaseItemDetail knowledgeBaseItemDetail = new KnowledgeBaseItemDetail();
            knowledgeBaseItemDetail.knowledgeBaseItem = item;
            await Navigation.PushAsync(knowledgeBaseItemDetail);
            ((ListView)sender).SelectedItem = null;
        }
    }
}