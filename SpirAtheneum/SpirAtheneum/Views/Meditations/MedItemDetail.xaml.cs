using Services.Models.Meditation;
using SpirAtheneum.ViewModels.MeditationViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.Meditations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedItemDetail : ContentPage
    {
        MeditationModel medItem;
        MeditationItemDetailVM medVM;

        public MedItemDetail(MeditationModel item)
        {
            InitializeComponent();
            medItem = item;
           
            medVM = new MeditationItemDetailVM();
            BindingContext = medVM;
           
        }

        public async void FetchAllMeditationsItems()
        {
            medVM.IsBusy = true;
            MeditationModel response = await medVM.FetchAllMeditationDetailItem(medItem.id);
            if (response != null)
            {
                needToShowHideLayout.IsVisible = true;
         
                UpdatePage(response);
                //todo
            }
            else {
                needToShowHideLayout.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Category list item is empty");

            }
            medVM.IsBusy = false;
        }
        private void UpdatePage(MeditationModel response)
        {
            Title = response.title;
            medVM.MedItem = response;
           
        }
        protected override void OnAppearing()
        {
            FetchAllMeditationsItems();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            medVM.IsBusy = false;
            NoDataLabel.IsVisible = false;
            base.OnDisappearing();
        }

        private void stepsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}