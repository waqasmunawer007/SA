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

        public UserViewModel(INavigation nav)
        {
            navigation = nav;
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<User>();
            user = new User();
            AddButtonCommand = new Command((e)=> {
                UserViewModel userViewModel = e as UserViewModel;
                User  u = userViewModel.DBUser;
                SaveUser(u);
            });
            LoginButtonCommand = new Command((e) => {
                UserViewModel userViewModel = e as UserViewModel;
                User u = userViewModel.DBUser;
                LoginUser(u);
            });
            tapCommand = new Command(OnTapped);
        }
        public User DBUser { get { return user; } set { this.user = value; OnPropertyChanged("DBUser"); } }
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
        /// Performs sign up for new user 
        /// </summary>
        /// <param name="u">U.</param>
        public  void  SaveUser(User u) 
        {
            bool ifRegistered = false;
            lock (collisionLock)
            {
                if (isUserExist(u) == false) //user not exist in db, create new user
                {
                    database.Insert(u);
                    ifRegistered = true; 

                }
            }
            if (ifRegistered)
            {
                LoginUser(u);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.RegistrarionError, AppConstant.Done);  
            }
			
        }
        public void LoginUser(User u)
        {
            lock (collisionLock)
            {
                if (isUserCredentialMatchToLogin(u) == true)
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
        }

        public bool isUserCredentialMatchToLogin(User u) 
        {
            lock (collisionLock) 
            {
                return database.Table<User>().Any(user => user.Email == u.Email && user.Password == u.Password);
            }
        }
        public bool isUserExist(User u)  
        {
            lock (collisionLock)
            {
                return database.Table<User>().Any(user => user.Email == u.Email );
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }



    }
}
