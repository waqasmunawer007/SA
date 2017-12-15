using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Services.Models.MobileUser;
using Services.Services.MobileUser;
using SpirAtheneum.AppUtils;
using SpirAtheneum.Constants;
using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.Account
{
    public class ChagePasswordViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ChangePasswordCommand { get; set; }
        INavigation navigation;

        bool isBusy;
        private string currentPassword = "";
        private string newPassword = "";
        private string confirmPassword = "";
      

        public ChagePasswordViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            SetupCommands();
        }

        private void SetupCommands()
        {
            ChangePasswordCommand = new Command(async(e) => {
                if (!string.Equals(NewPassword, String.Empty) && !string.Equals(ConfirmPassword, String.Empty))
                {
                    if (NewPassword.Equals(ConfirmPassword))
                    {
                        var bytes = Util.EncryptAes(NewPassword);
                        string encryptedNewPassword = BitConverter.ToString(bytes);

                        //Todo remove once password field is added on the server side
                        //var mobileUserService = new MobileUserService();
                        //AppMobileUser user = new AppMobileUser();
                        //user.password = encryptedNewPassword;
                        //AppMobileUser updatedUser  = await mobileUserService.UpdateMobileUser(user);

                        //update password in local database
                        DatabaseHelper.GetInstance().ChangePassword(NewPassword);
                        await Application.Current.MainPage.DisplayAlert("", AppConstant.SuccessPasswordChange, AppConstant.Done);
                        await navigation.PopModalAsync();
                       


                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", AppConstant.PasswordUnmatchedError, AppConstant.Done);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("",AppConstant.PasswordEmpityFieldError, AppConstant.Done);
                }
              
            });
           
        }


        /// <summary>
        /// Creates the new mobile user on the server
        /// </summary>
        /// <param name="subscriptionId">Subscription identifier.</param>
        private async Task CreateMobileUser(string subscriptionId)
        {
            IsBusy = true;

            //await PostUserFevorite(); //Post users fevourites if available
            IsBusy = false;


        }

        #region Bindable Properties
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChanged("NewPassword");
                }
            }
        }
        public string CurrentPassword
        {
            get { return currentPassword; }
            set
            {
                if (currentPassword != value)
                {
                    currentPassword = value;
                    OnPropertyChanged("CurrentPassword");
                }
            }
        }
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
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
