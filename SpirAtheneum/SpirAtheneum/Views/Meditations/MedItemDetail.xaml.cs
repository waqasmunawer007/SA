using Services.Models.Meditation;
using SpirAtheneum.Models;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            FetchItemDetail();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
