using Services.Models.KnowledgeBase;
using SpirAtheneum.ViewModels.KnowledgeBaseViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.KnowledgeBase
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KnowledgeBaseItems : ContentPage
	{
        KnowledgeBaseItemsVM knowledgeBaseVM;

        string selectCategory;
        public KnowledgeBaseItems(string category)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            knowledgeBaseVM = new KnowledgeBaseItemsVM(category);
            BindingContext = knowledgeBaseVM;
            listView.ItemsSource = knowledgeBaseVM.knowledgeBaseList;
            selectCategory = category;
            Title = category;
        }

        public async void FetchAllItems()
        {
            knowledgeBaseVM.IsBusy = true;
            List<KnowledgeBaseModel> categoryItems = await knowledgeBaseVM.FetchAllCategoryItems();
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
            knowledgeBaseVM.IsBusy = false;

        }

        private void UpdatePage(List<KnowledgeBaseModel> data)
        {
            foreach (KnowledgeBaseModel k in data)
            {
                knowledgeBaseVM.knowledgeBaseList.Add(k);
            }
        }

        protected override void OnAppearing()
        {
            FetchAllItems();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            knowledgeBaseVM.knowledgeBaseList.Clear();
            NoDataLabel.IsVisible = false;
            knowledgeBaseVM.IsBusy = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            KnowledgeBaseModel item = (KnowledgeBaseModel)selectedCategory;
            await Navigation.PushAsync(new KnowledgeBaseItemDetail(item));
            ((ListView)sender).SelectedItem = null;
        }
    }
}