using Plugin.FirebasePushNotification;
using Plugin.LocalNotifications;
using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using SpirAtheneum.Interfaces;
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
            DatabaseHelper.GetInstance().CreateDatabase();

            initPushNotificationListner(); // init the push notification event listeners
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
        private void initPushNotificationListner()
        {
            /// <summary>
            /// Event triggered when token is refreshed
            /// </summary>
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };

            /// <summary>
            /// Event triggered when a notification is received
            /// </summary>
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                CrossLocalNotifications.Current.Show("title", "Received");
                
                System.Diagnostics.Debug.WriteLine("Received");

            };

            /// <summary>
            /// Event triggered when a notification is opened
            /// </summary>
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                }

            };

        }
    }
}
