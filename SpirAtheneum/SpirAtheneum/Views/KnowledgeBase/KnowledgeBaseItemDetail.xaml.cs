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
	public partial class KnowledgeBaseItemDetail : ContentPage
	{
        KnowledgeBaseModel knowledgeBaseItem;
        KnowledgeBaseItemDetailVM knowledgeBaseVM;

        public KnowledgeBaseItemDetail(KnowledgeBaseModel item)
        {
            InitializeComponent();
            knowledgeBaseItem = item;
            knowledgeBaseVM = new KnowledgeBaseItemDetailVM();
            BindingContext = knowledgeBaseVM;
        }

        public async void FetchItemDetail()
        {
            knowledgeBaseVM.IsBusy = true;
            KnowledgeBaseModel response = await knowledgeBaseVM.FetchKnowledgeBaseItemDetail(knowledgeBaseItem.id);
            if (response != null)
            {
                needToShowHideLayout.IsVisible = true;

                UpdatePage(response);
                //todo
            }
            else
            {
                needToShowHideLayout.IsVisible = false;
                NoDataLabel.IsVisible = true;
                Debug.WriteLine("Category list item is empty");

            }
            knowledgeBaseVM.IsBusy = false;
        }
        private void UpdatePage(KnowledgeBaseModel response)
        {
            Title = response.title;
            knowledgeBaseVM.Item = response;

        }
        protected override void OnAppearing()
        {
            FetchItemDetail();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            knowledgeBaseVM.IsBusy = false;
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