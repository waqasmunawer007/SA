using SpirAtheneum.Models;
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
	public partial class Categories : ContentPage
	{
        KnowledgeBaseVM knowledgeBaseVM;
        public Categories()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            knowledgeBaseVM = new KnowledgeBaseVM();
            BindingContext = knowledgeBaseVM;
            listView.ItemsSource = knowledgeBaseVM.knowledgeBaseList;
            Title = "All Categories";

        }
        public async void FetchAllKnowledgeBaseAsync()
        {
            knowledgeBaseVM.IsBusy = true;
            List<Category> knowledgeBaseCategories = await knowledgeBaseVM.FetchAllKnowledgeBaseCategory();

            if (knowledgeBaseCategories != null && knowledgeBaseCategories.Count > 0)
            {
                listView.IsVisible = true;
                UpdatePage(knowledgeBaseCategories);
            }
            else
            {
                listView.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("KnowledgeBase category list is empty");

            }
            knowledgeBaseVM.IsBusy = false;
        }



        private void UpdatePage(List<Category> data)
        {
            foreach (Category k in data)
            {
                knowledgeBaseVM.knowledgeBaseList.Add(k);
            }
        }
        protected override void OnAppearing()
        {
            FetchAllKnowledgeBaseAsync();
            base.OnAppearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            Category category = (Category)selectedCategory;
            await Navigation.PushAsync(new KnowledgeBaseItems(category.category));
            ((ListView)sender).SelectedItem = null;

        }
        protected override void OnDisappearing()
        {
            knowledgeBaseVM.knowledgeBaseList.Clear();
            NoDataLabel.IsVisible = false;
            knowledgeBaseVM.IsBusy = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }

    }
}