using Services.Models.Meditation;
using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

namespace SpirAtheneum.Views.Meditations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedItemDetail : ContentPage
    {
        public MeditationBinding meditationItem;
        MeditationItemDetailVM meditationVM;

        public MedItemDetail()
        {
            InitializeComponent();
			ToolbarItems.Add(new ToolbarItem("", "icon_home_white.png", () =>
			{
				Application.Current.MainPage = new Views.Menu.MainPage();
			}));
            meditationVM = new MeditationItemDetailVM();
            BindingContext = meditationVM;
        }

        public void FetchItemDetail()
        {
            MeditationBinding response = meditationVM.FetchMeditationItemDetail(meditationItem.id);
            if (response != null)
            {
                UpdatePage(response);
                //todo
            }
            else
            {
                Debug.WriteLine("Category list item is empty");
            }
        }

        private void UpdatePage(MeditationBinding response)
        {
            Title = response.title;
            meditationVM.Item = response;
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
            FetchItemDetail();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
