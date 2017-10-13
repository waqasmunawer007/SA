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

namespace SpirAtheneum.ViewModels
{
    class UserViewModel : INotifyPropertyChanged
    {

        private SQLiteConnection database;
        private static object collisionLock = new object();
        public User user;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddButtonCommand { get; set; }
        public ICommand LoginButtonCommand { get; set; }
        public ICommand tapCommand;

        public UserViewModel()
        {
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
            App.Current.MainPage = new Signup();
            Settings.Login = 0;

        }
       

       public void  SaveUser(User u)  // save user in local database
        {
            lock (collisionLock)
            {
                if (isUserExist(u) == false)
                {
                    database.Insert(u);
                    Settings.Login = 1;
                    App.Current.MainPage = new Views.Menu.MainPage();
                   
                }
                else
                {
                    App.Current.MainPage = new LoginPage();
                    Settings.Login = 0;
                    
                }

            }
           
        }
        public void LoginUser(User u)
        {
            lock (collisionLock)
            {
                if (isUserCredentialMatchToLogin(u) == true)
                {
                    Settings.Login = 1;
                    App.Current.MainPage  = new Views.Menu.MainPage();
                }
                else
                {
                    App.Current.MainPage = new LoginPage();
                    Settings.Login = 0;
                    
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
