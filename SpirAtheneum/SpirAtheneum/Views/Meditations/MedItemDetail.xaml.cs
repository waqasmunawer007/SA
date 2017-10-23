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
        Meditation meditationItem;
        MeditationItemDetailVM meditationVM;

        public MedItemDetail(Meditation item)
        {
            InitializeComponent();
            meditationItem = item;
            meditationVM = new MeditationItemDetailVM();
            BindingContext = meditationVM;
        }

        public void FetchItemDetail()
        {
            Meditation response = meditationVM.FetchMeditationItemDetail(meditationItem.id);
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

        private void UpdatePage(Meditation response)
        {
            Title = response.title;
            meditationVM.Item = response;
            var e = meditationVM.Item.intro;
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
