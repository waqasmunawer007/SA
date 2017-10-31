using SpirAtheneum.ViewModels.KnowledgeBaseViewModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.KnowledgeBase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KnowledgeBaseItemDetail : ContentPage
	{
        public Models.KnowledgeBaseBinding knowledgeBaseItem;
        KnowledgeBaseItemDetailVM knowledgeBaseVM;

        public KnowledgeBaseItemDetail()
        {
            InitializeComponent();
            knowledgeBaseVM = new KnowledgeBaseItemDetailVM();
            BindingContext = knowledgeBaseVM;
        }

        public void FetchItemDetail()
        {
            Models.KnowledgeBaseBinding response = knowledgeBaseVM.FetchKnowledgeBaseItemDetail(knowledgeBaseItem.id);
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

        private void UpdatePage(Models.KnowledgeBaseBinding response)
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
            base.OnDisappearing();
        }
    }
}