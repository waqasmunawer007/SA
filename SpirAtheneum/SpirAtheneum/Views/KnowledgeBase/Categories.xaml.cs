using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.KnowledgeBaseViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

namespace SpirAtheneum.Views.KnowledgeBase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Categories : ContentPage
	{
        KnowledgeBaseVM knowledgeBaseVM;
        int flag;
        public Categories(int? i = 0)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            knowledgeBaseVM = new KnowledgeBaseVM();
            BindingContext = knowledgeBaseVM;
            listView.ItemsSource = knowledgeBaseVM.knowledgeBaseList;
            Title = "All Categories";
            listViewStackLayout.Margin = new Thickness(0, -10, 0, 0);
            flag = 0;
            //show hide toolbar
            if (i == 1)
                toolbar.IsVisible = true;
            else
                toolbar.IsVisible = false;
        }
        public Categories()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            knowledgeBaseVM = new KnowledgeBaseVM();
            BindingContext = knowledgeBaseVM;
            listView.ItemsSource = knowledgeBaseVM.knowledgeBaseList;
            Title = "All Categories";
            listViewStackLayout.Margin = new Thickness(0, 10, 0, 0);
            flag = 1;
            toolbar.IsVisible = false;
        }

        public async void FetchAllKnowledgeBaseAsync()
        {
            knowledgeBaseVM.IsBusy = true;
            List<Category> knowledgeBaseCategories = await knowledgeBaseVM.DatabaseOperation();

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
            //to give some space between (bottom side) toolbar and list 
            if (flag == 1)
            {
                listViewStackLayout.Margin = new Thickness(0, 10, 0, 0);
            }
            if (Settings.IsSubscriped)
            {
                ADMob.IsVisible = false;
            }
            else
            {
                ADMob.IsVisible = true;
            }
            base.OnAppearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            Category category = (Category)selectedCategory;
            await Navigation.PushModalAsync(new KnowledgeBaseItems(category.category));
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnDisappearing()
        {
            //small hack to avoid space issue comming from hamberger menu
            if (flag == 1)
            {
                listViewStackLayout.Margin = new Thickness(0, -10, 0, 0);
            }
            knowledgeBaseVM.knowledgeBaseList.Clear();
            NoDataLabel.IsVisible = false;
            knowledgeBaseVM.IsBusy = false;
            ADMob.IsVisible = false;
            listView.IsVisible = false;
            base.OnDisappearing();
        }
        private void BackTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}