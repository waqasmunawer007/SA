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
using System.Text.RegularExpressions;
using Services.Services.Signup;

namespace SpirAtheneum.ViewModels
{
    class UserVM : INotifyPropertyChanged
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
        public bool subscription = true; //if app is subscribe

        public UserVM(INavigation nav)
        {
            navigation = nav;
            databaseHelper = new DatabaseHelper();
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            user = new User();

            AddButtonCommand = new Command((e) => {
                UserVM userViewModel = e as UserVM;
                User u = userViewModel.User;
                if(subscription == true)
                {
                    SignupUserForLocally(u);
                    SignupUserForServer(u);
                }
                else
                {
                    SignupUserForLocally(u);
                }
            });

            LoginButtonCommand = new Command((e) => {
                UserVM userViewModel = e as UserVM;
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
           

            if(u.Email != null && u.Email != "" && u.Password != null && u.Password != "")
            {
                if (Regex.IsMatch(u.Email, AppConstant.EmailPatteren))
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
                else
                {
                    ShowError = true;
                    Message = "Please enter a valid email !";
                }
            }
            else
            {
                ShowError = true;
                Message = "Email and Password cannot be empty !";
            }
        }

        /// <summary>
        /// This function is used to signup user for local DB
        /// </summary>
        /// <param name="u"></param>
        public void SignupUserForLocally(User u)
        {
            if (u.Email != null && u.Email != "" && u.Password != null && u.Password != "")
            {
                if (Regex.IsMatch(u.Email, AppConstant.EmailPatteren))
                {
                    if (databaseHelper.AddUser(u))
                    {
                        //LoginUser(u);
                        Settings.IsLogin = true;
                        App.Current.MainPage = new Views.Menu.MainPage();
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert(AppConstant.Sorry, AppConstant.RegistrarionError, AppConstant.Done);
                    }
                }
                else
                {
                    ShowError = true;
                    Message = APIsConstant.InvalidEmail;
                }
            }
            else
            {
                ShowError = true;
                Message = APIsConstant.EmptyEmailAndPassword;
            }
        }

        /// <summary>
        /// This function is used to signup user for Server DB
        /// </summary>
        /// <param name="u"></param>
        public async void SignupUserForServer(User u)
        {
            if (u.Email != null && u.Email != "" && u.Password != null && u.Password != "")
            {
                if (Regex.IsMatch(u.Email, AppConstant.EmailPatteren))
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add(APIsConstant.Email, u.Email);
                    parameters.Add(APIsConstant.Password, u.Password);
                    var sigupService = new SignupService();
                    var signupResponse = await sigupService.Signup(parameters);
                    if (signupResponse != null)
                    {
                        if (signupResponse == "true")
                        {
                            Settings.IsLogin = true;
                            App.Current.MainPage = new Views.Menu.MainPage();
                        }
                        else if (signupResponse == "false")
                        {
                            await App.Current.MainPage.DisplayAlert(APIsConstant.SignupNullResponseTitle,APIsConstant.SignupNullResponse,APIsConstant.OK);
                        }
                    }
                }
                else
                {
                    ShowError = true;
                    Message = APIsConstant.InvalidEmail;
                }
            }
            else
            {
                ShowError = true;
                Message = APIsConstant.EmptyEmailAndPassword;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
