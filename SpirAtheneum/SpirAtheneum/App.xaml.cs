using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using SpirAtheneum.Views.Login;
using SpirAtheneum.Views.Meditations;
using Xamarin.Forms;

namespace SpirAtheneum
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DatabaseHelper databaseHelper = new DatabaseHelper();
            databaseHelper.CreateDatabase();

            if (Settings.IsLogin)
            {
                 MainPage = new Views.Menu.MainPage();  
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
