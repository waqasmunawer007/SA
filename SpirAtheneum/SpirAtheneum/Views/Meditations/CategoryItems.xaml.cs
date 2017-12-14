using Services.Models.Meditation;
using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

namespace SpirAtheneum.Views.Meditations.CategoryItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryItems : ContentPage
    {
        MeditationItemsVM meditationVM;
        string selectCategory;

        public CategoryItems(string category)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
			//ToolbarItems.Add(new ToolbarItem("", "icon_home_white.png", () =>
			//{
			//	//logic code goes here
			//	Application.Current.MainPage = new Views.Menu.MainPage();
			//}));
            meditationVM = new MeditationItemsVM(category);
            BindingContext = meditationVM;
            listView.ItemsSource = meditationVM.MeditationBinding;
            toolbar_title.Text = category;
            toolbar.IsVisible = false;
        }

       public void FetchAllItems()
       {
            List<MeditationBinding> categoryItems = meditationVM.FetchAllCategoryItems();
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

        private void UpdatePage(List<MeditationBinding> data)
        {
            foreach (MeditationBinding m in data)
            {
                meditationVM.MeditationBinding.Add(m);
            }
        }

        protected override void OnAppearing()
        {
			
            FetchAllItems();
            toolbar.IsVisible = true;
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

        protected override void OnDisappearing()
        {
            meditationVM.MeditationBinding.Clear();
            listView.IsVisible = false;
            ADMob.IsVisible = false;
            base.OnDisappearing();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedCategory = ((ListView)sender).SelectedItem;
            MeditationBinding item = (MeditationBinding)selectedCategory;
            MedItemDetail medItemDetail = new MedItemDetail();
            medItemDetail.meditationItem = item;
            await Navigation.PushModalAsync(medItemDetail,false);
            ((ListView)sender).SelectedItem = null;
        }
        private void BackTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private void HomeTapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Views.Menu.MainPage();
        }
    }
}