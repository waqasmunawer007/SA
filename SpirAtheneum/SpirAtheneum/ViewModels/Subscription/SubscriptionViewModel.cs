using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Services.Models.Favourite;
using Services.Models.MobileUser;
using Services.Models.Subscription;
using Services.Services.MobileUser;
using Services.Services.SubScription;
using SpirAtheneum.Constants;
using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using SpirAtheneum.Models;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.Subscription
{
	public class SubscriptionViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        public AppSubscription[] subscriptionList;
		
		public ICommand MonthlySubscriptionCommand { get; set; }
        public ICommand YearlySubscriptionCommand { get; set; }
        private bool isBusy,isConatinerVisible = false;
        private string monthlySubscription = "";
        private string yearlySubscription = "";
        private string monthlyDesc = "";
        private string yearlyDesc = "";

		public SubscriptionViewModel()
		{
            SetupCommands();
        }

        private void SetupCommands()
        {
            MonthlySubscriptionCommand = new Command((e) => {
                Settings.IsSubscriped = true;
                CreateMobileUser(subscriptionList[0].id);
                Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.SubscriptionSuccess, AppConstant.Done);

            });
            YearlySubscriptionCommand = new Command((e) => {
                Settings.IsSubscriped = true;
                CreateMobileUser(subscriptionList[1].id);
                Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.SubscriptionSuccess, AppConstant.Done);
            }); 
        }

        private async void CreateMobileUser(string subscriptionId)
        {
            var mobileService = new MobileUserService();
            Dictionary < string, object> parameters = new Dictionary<string, object>();
            parameters.Add("email",Settings.Email);
            parameters.Add("is_active", false);
            parameters.Add("subscription_type_id", subscriptionId);
            parameters.Add("created_at", DateTime.Now);
            AppMobileUser user =   await mobileService.CreateMobileUser(parameters);
           
            Debug.WriteLine("User",user.email);

        }
        private void PostUserFevorite()
        {
            List<FavouriteMeditation> fevMeditations = DatabaseHelper.GetInstance().GetMeditationFavourite();
            List<FavouriteKnowledgeBase> fevKB = DatabaseHelper.GetInstance().GetKnowledgeBaseFavourite();
            FevouriteRequest fevRequest = new FevouriteRequest();

        }

        #region Bindable Properties
        public string MonthlyDesc
        {
            get { return monthlyDesc; }
            set
            {
                if (monthlyDesc != value)
                {
                    monthlyDesc = value;
                    OnPropertyChanged("MonthlyDesc");
                }
            }
        }
        public string YearlyDesc
        {
            get { return yearlyDesc; }
            set
            {
                if (yearlyDesc != value)
                {
                    yearlyDesc = value;
                    OnPropertyChanged("YearlyDesc");
                }
            }
        }

        public string MonthlySubscription
        {
            get { return monthlySubscription; }
            set
            {
                if (monthlySubscription != value)
                {
                    monthlySubscription = value;
                    OnPropertyChanged("MonthlySubscription");
                }
            }
        }

        public string YearlySubscription
        {
            get { return yearlySubscription; }
            set
            {
                if (yearlySubscription != value)
                {
                    yearlySubscription = value;
                    OnPropertyChanged("YearlySubscription");
                }
            }
        }
        public bool IsConatinerVisible
		{
            get { return isConatinerVisible; }
			set
			{
                if (isConatinerVisible != value)
				{
                    isConatinerVisible = value;
                    OnPropertyChanged("IsConatinerVisible");
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

        public async Task FetchSubscriptionItems()
		{
            IsBusy = true;
			var subscriptionService = new SubscriptionService();
            subscriptionList = await subscriptionService.GetAppSubscriptionList();
            if (subscriptionList != null && subscriptionList.Length > 0)
			{

                AppSubscription monthSubscription = subscriptionList[0];
                AppSubscription yearSubscription = subscriptionList[1];

                MonthlySubscription = "$" + monthSubscription.cost + "/" + monthSubscription.name;
                MonthlyDesc = "-"+monthSubscription.description;
           
                YearlySubscription = "$" + yearSubscription.cost + "/"+yearSubscription.name;
                YearlyDesc = "-"+yearSubscription.description;
			}
			else
			{
				Debug.WriteLine("Subscription list is empty");
				

			}
            IsBusy = false;
            IsConatinerVisible = true;
		}

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
