using SpirAtheneum.Helpers;
using SpirAtheneum.Interfaces;
using SpirAtheneum.Models;
using SpirAtheneum.Views;
using SpirAtheneum.Views.Login;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SpirAtheneum.Constants;
using SpirAtheneum.Database;

namespace SpirAtheneum.ViewModels
{
    class UserViewModel : INotifyPropertyChanged
    {

        private SQLiteConnection database;
        private static object collisionLock = new object();
        public User user;
        private bool showError;
        private bool showRegistrationMessage;
        private string message; //could be an error or a success message
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddButtonCommand { get; set; }
        public ICommand LoginButtonCommand { get; set; }
        public ICommand tapCommand;
        INavigation navigation;
        public DatabaseHelper databaseHelper;

        public UserViewModel(INavigation nav)
        {
            navigation = nav;
            databaseHelper = new DatabaseHelper();
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            user = new User();

            AddButtonCommand = new Command((e) => {
                UserViewModel userViewModel = e as UserViewModel;
                User u = userViewModel.User;
                SignupUser(u);
            });

            LoginButtonCommand = new Command((e) => {
                UserViewModel userViewModel = e as UserViewModel;
                User u = userViewModel.User;
                LoginUser(u);
            });

            tapCommand = new Command(OnTapped);
        }

        public User User { get { return user; } set { this.user = value; OnPropertyChanged("User"); } }

        public ICommand TabCommand { get { return tapCommand; } set { tapCommand = value; } }

        void OnTapped(object s)   // Sign up button command
        {
            navigation.PushAsync(new SignUp());
            // Settings.Login = 0;
        }

        public bool ShowError
        {
            get { return showError; }
            set
            {
                if (showError != value)
                {
                    showError = value;
                    OnPropertyChanged("ShowError");
                }
            }
        }

        public bool ShowRegistrationMessage
        {
            get { return showRegistrationMessage; }
            set
            {
                if (showRegistrationMessage != value)
                {
                    showRegistrationMessage = value;
                    OnPropertyChanged("ShowRegistrationMessage");
                }
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        /// <summary>
        /// This function is used to login user
        /// </summary>
        /// <param name="u"></param>
        public void LoginUser(User u)
        {
            if (databaseHelper.GetUser(u))
            {
                Settings.IsLogin = true;
                App.Current.MainPage = new Views.Menu.MainPage();
            }
            else
            {
                ShowError = true;
                Message = AppConstant.LoginError;
            }
        }

        /// <summary>
        /// This function is used to signup user
        /// </summary>
        /// <param name="u"></param>
        public void SignupUser(User u)
        {
            if (databaseHelper.AddUser(u))
            {
                LoginUser(u);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.RegistrarionError, AppConstant.Done);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
