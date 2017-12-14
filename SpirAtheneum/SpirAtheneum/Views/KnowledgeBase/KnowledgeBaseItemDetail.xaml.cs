using SpirAtheneum.ViewModels.KnowledgeBaseViewModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpirAtheneum.Helpers;

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
			ToolbarItems.Add(new ToolbarItem("", "icon_home_white.png", () =>
			{
				//logic code goes here
				Application.Current.MainPage = new Views.Menu.MainPage();
			}));
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
            base.OnDisappearing();
            ADMob.IsVisible = false;
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