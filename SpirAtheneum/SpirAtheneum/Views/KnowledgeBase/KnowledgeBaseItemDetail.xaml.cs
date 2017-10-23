using SpirAtheneum.ViewModels.KnowledgeBaseViewModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpirAtheneum.Views.KnowledgeBase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KnowledgeBaseItemDetail : ContentPage
	{
        Models.KnowledgeBase knowledgeBaseItem;
        KnowledgeBaseItemDetailVM knowledgeBaseVM;

        public KnowledgeBaseItemDetail(Models.KnowledgeBase item)
        {
            InitializeComponent();
            knowledgeBaseItem = item;
            knowledgeBaseVM = new KnowledgeBaseItemDetailVM();
            BindingContext = knowledgeBaseVM;
        }

        public void FetchItemDetail()
        {
            Models.KnowledgeBase response = knowledgeBaseVM.FetchKnowledgeBaseItemDetail(knowledgeBaseItem.id);
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

        private void UpdatePage(Models.KnowledgeBase response)
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