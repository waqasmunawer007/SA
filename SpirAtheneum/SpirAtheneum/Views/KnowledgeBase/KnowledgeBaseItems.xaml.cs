using SpirAtheneum.ViewModels.KnowledgeBaseViewModel;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void FetchAllItems()
        {
            List<Models.KnowledgeBase> categoryItems = knowledgeBaseVM.FetchAllCategoryItems();
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

        private void UpdatePage(List<Models.KnowledgeBase> data)
        {
            foreach (Models.KnowledgeBase k in data)
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
            listView.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            Models.KnowledgeBase item = (Models.KnowledgeBase)selectedCategory;
            await Navigation.PushAsync(new KnowledgeBaseItemDetail(item));
            ((ListView)sender).SelectedItem = null;
        }
    }
}