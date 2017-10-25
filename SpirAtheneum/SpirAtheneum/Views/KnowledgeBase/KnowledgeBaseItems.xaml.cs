using SpirAtheneum.ViewModels.KnowledgeBaseViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.KnowledgeBase
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
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
            listView.ItemsSource = knowledgeBaseVM.KnowledgeBaseBinding;
            selectCategory = category;
            Title = category;
        }

        public void FetchAllItems()
        {
            List<Models.KnowledgeBaseBinding> categoryItems = knowledgeBaseVM.FetchAllCategoryItems();
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

        private void UpdatePage(List<Models.KnowledgeBaseBinding> data)
        {
            foreach (Models.KnowledgeBaseBinding k in data)
            {
                knowledgeBaseVM.KnowledgeBaseBinding.Add(k);
            }
        }

        protected override void OnAppearing()
        {
            FetchAllItems();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            knowledgeBaseVM.KnowledgeBaseBinding.Clear();
            listView.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            Models.KnowledgeBaseBinding item = (Models.KnowledgeBaseBinding)selectedCategory;
            KnowledgeBaseItemDetail knowledgeBaseItemDetail = new KnowledgeBaseItemDetail();
            knowledgeBaseItemDetail.knowledgeBaseItem = item;
            await Navigation.PushAsync(knowledgeBaseItemDetail);
            ((ListView)sender).SelectedItem = null;
        }
    }
}