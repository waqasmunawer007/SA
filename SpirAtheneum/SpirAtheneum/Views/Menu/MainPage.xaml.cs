using SpirAtheneum.Helpers;
using SpirAtheneum.Models;
using SpirAtheneum.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using System.Net;
using SpirAtheneum.Interfaces;
using Plugin.Messaging;
using Xamarin.Forms.Internals;
using Plugin.DeviceInfo;
using SpirAtheneum.Constants;

namespace SpirAtheneum.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                if (item.TargetType == typeof(Login.LoginPage))
                {

                    Settings.IsLogin = false;
                    App.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else if (item.TargetType == typeof(ContactUs.ContactUs))
                {
                    string appName = AppConstant.AppName;
                    string modle = CrossDeviceInfo.Current.Model;
                    var versionNumber = CrossDeviceInfo.Current.VersionNumber;
                    var platform = CrossDeviceInfo.Current.Platform;
                    string subject = appName + " | " + platform + " | " + versionNumber + " | " + modle;

                    var email = new EmailMessageBuilder()
                    .To(AppConstant.AdminEmail)
                    .Subject(subject)
                    .Build();
                    var emailTask = CrossMessaging.Current.EmailMessenger;
					emailTask.SendEmail(email);
                    if (emailTask.CanSendEmail)
                    {
                        emailTask.SendEmail(email);
                    }
                }
                else
                {

                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    MasterPage.ListView.SelectedItem = null;
                   
                    IsPresented = false;
                }
            }
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}