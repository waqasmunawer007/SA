using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SpirAtheneum.Constants;
using SpirAtheneum.Helpers;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.Account
{
    public class AccountViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ChangeSubscriptionCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        INavigation navigation;

        bool isBusy;
        private string email = Settings.Email;
        private string subscriptionPrice = "$"+Settings.SubscriptionPrice;
       

        public AccountViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            SetupCommands();
        }

        private void SetupCommands()
        {
            ChangeSubscriptionCommand = new Command(async (e) => {
                await navigation.PushModalAsync(new ChangeSubscriptionPage());
            });
            ChangePasswordCommand = new Command( (e) => {
                ChangePasswordPage changePasswordPage = new ChangePasswordPage();
                navigation.PushModalAsync(changePasswordPage);
                //await Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.SubscriptionSuccess, AppConstant.Done);
            });
        }
        #region Bindable Properties
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string SubscriptionPrice
        {
            get { return subscriptionPrice; }
            set
            {
                if (subscriptionPrice != value)
                {
                    subscriptionPrice = value;
                    OnPropertyChanged("SubscriptionPrice");
                }
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }
        #endregion


        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
