using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Services.Models.Favourite;
using Services.Models.KnowledgeBase;
using Services.Models.Meditation;
using Services.Models.MobileUser;
using Services.Models.Subscription;
using Services.Services.Favourite;
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

        public ICommand ChangeMonthlySubscriptionCommand { get; set; }
        public ICommand ChangeYearlySubscriptionCommand { get; set; }

        public ICommand CancelSubscriptionCommand { get; set; }
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
            MonthlySubscriptionCommand = new Command(async (e) => {
                await CreateMobileUser(subscriptionList[0].id,subscriptionList[0].cost);
                await Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.SubscriptionSuccess, AppConstant.Done);
            });
            YearlySubscriptionCommand = new Command(async (e) => {
             
                await CreateMobileUser(subscriptionList[1].id,subscriptionList[1].cost);
                await Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.SubscriptionSuccess, AppConstant.Done);
            }); 

            ChangeMonthlySubscriptionCommand = new Command(async (e) => {
                await ChangeSubscription(subscriptionList[0].id,subscriptionList[0].cost);
                await Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.SubscriptionChangeSuccess, AppConstant.Done);

            });
            ChangeYearlySubscriptionCommand = new Command(async (e) => {
               
                await ChangeSubscription(subscriptionList[1].id,subscriptionList[1].cost);
                await Application.Current.MainPage.DisplayAlert(AppConstant.Congratulation, AppConstant.SubscriptionChangeSuccess, AppConstant.Done);
            }); 
            CancelSubscriptionCommand = new Command(async (e) => {
                bool answer= await Application.Current.MainPage.DisplayAlert("", AppConstant.CancelSubscriptionAlert, "Yes","Cancel");
                if (answer)
                {
                    //Settings.IsSubscriped = false;
                    //Settings.SubscriptionPrice = 0.0;
                    await ChangeSubscription("",0.0);
                    await Application.Current.MainPage.DisplayAlert("", AppConstant.CancelSubscription, AppConstant.Done);
                }
            }); 
        }
        /// <summary>
        /// Updates the subscription in local db.
        /// </summary>
        /// <param name="subscriptionStatus">If set to <c>true</c> subscription status.</param>
        /// <param name="subScriptionPrice">Sub scription price.</param>
        private void UpdateSubscriptionInLocalDB(bool subscriptionStatus, double subScriptionPrice, string mobileUserId, string fevId)
        {
            DatabaseHelper.GetInstance().UpdateUserSubscription(subscriptionStatus, subScriptionPrice, mobileUserId, fevId);
        }
        /// <summary>
        /// Changes the subscription.
        /// </summary>
        /// <returns>The subscription.</returns>
        /// <param name="subscriptionId">Subscription identifier.</param>
        private async Task ChangeSubscription(string subscriptionId,double subscriptionPrice)
        {
            IsBusy = true;
            var mobileService = new MobileUserService();
            AppMobileUser mobileUser = new AppMobileUser();
            mobileUser.id = Settings.MobileUserId;
            mobileUser.email = Settings.Email;
            mobileUser.pass_hash = Settings.Password;
            mobileUser.favorites_id = Settings.FevouriteId;
            mobileUser.subscription_type_id = subscriptionId;
            mobileUser.created_at = DateTime.Now.ToString();
            Services.Models.Subscription.Meta meta = new Services.Models.Subscription.Meta();
            meta.author = AppConstant.AppAuther;
            meta.date_added = DateTime.Now.ToString();
            meta.last_edited = DateTime.Now.ToString();
            mobileUser.meta = meta;

            if (!String.IsNullOrEmpty(subscriptionId))
            {
                mobileUser.is_active = "true";
            }
            else
            {
                mobileUser.is_active = "false"; //susbcription has been cancelled
            }
            AppMobileUser user = await mobileService.UpdateMobileUser(mobileUser);
            if (!String.IsNullOrEmpty(subscriptionId))
            {
                Settings.MobileUserId = user.id;
                Settings.FevouriteId = user.favorites_id;
                Settings.IsSubscriped = true;
                Settings.SubscriptionPrice = subscriptionPrice;
                UpdateSubscriptionInLocalDB(true, subscriptionPrice, user.id, user.favorites_id);
            }
            else
            {
                Settings.IsSubscriped = false;
                Settings.SubscriptionPrice = 0.0;
                UpdateSubscriptionInLocalDB(false, 0.0, user.id, user.favorites_id);
            }
            IsBusy = false;
        }

        /// <summary>
        /// Creates the new mobile user on the server
        /// </summary>
        /// <param name="subscriptionId">Subscription identifier.</param>
        private async Task CreateMobileUser(string subscriptionId,double subscriptionPrice)
        {
            if (string.IsNullOrEmpty(Settings.FevouriteId)) //indicate the subcription is going to be happened very first time 
            {
                IsBusy = true;
                var mobileService = new MobileUserService();
                AppMobileUser mobileUser = new AppMobileUser();
                mobileUser.id = Guid.NewGuid().ToString();
                mobileUser.email = Settings.Email;
                mobileUser.pass_hash = Settings.Password;
                mobileUser.favorites_id = Guid.NewGuid().ToString();
                mobileUser.is_active = "true";
                mobileUser.subscription_type_id = subscriptionId;
                mobileUser.created_at = DateTime.Now.ToString();

                Services.Models.Subscription.Meta meta = new Services.Models.Subscription.Meta();
                meta.author = AppConstant.AppAuther;
                meta.date_added = DateTime.Now.ToString();
                meta.last_edited = DateTime.Now.ToString();
                mobileUser.meta = meta;

                AppMobileUser user = await mobileService.CreateMobileUser(mobileUser);
                Settings.MobileUserId = user.id;
                Settings.FevouriteId = user.favorites_id;
                Settings.IsSubscriped = true;
                Settings.SubscriptionPrice = subscriptionPrice;

                UpdateSubscriptionInLocalDB(true, subscriptionPrice, user.id, user.favorites_id);

                await PostUserFevorite(); //Post users fevourites if available
                IsBusy = false;
            }
            else
            {
                //Only update subscription status. don't create new user
                await ChangeSubscription(subscriptionId, subscriptionPrice);
            }



        }
        /// <summary>
        /// Posts the user fevorites to the server
        /// </summary>
        private async Task PostUserFevorite()
        {
            List<MeditationModel> fevMeditationList = new List<MeditationModel>();
            List<KnowledgeBaseModel> fevKnowledgeList = new List<KnowledgeBaseModel>();

            List<FavouriteMeditation> fevMeditations = DatabaseHelper.GetInstance().GetMeditationFavourite();
            List<FavouriteKnowledgeBase> fevKB = DatabaseHelper.GetInstance().GetKnowledgeBaseFavourite();

            if (fevMeditations != null && fevMeditations.Count > 0) //prepare required server Meditation model
            {
                foreach (FavouriteMeditation fevMedi in fevMeditations)
                {
                    if (fevMedi.is_favourite.Equals("true"))
                    {
                        MeditationModel meditation = new MeditationModel();
                        meditation.id = fevMedi.id;
                        fevMeditationList.Add(meditation);
                    }
                }
            }
            if (fevKB != null && fevKB.Count > 0) //prepare required server KB model
            {
                foreach (FavouriteKnowledgeBase fevkb in fevKB)
                {
                    if (fevkb.is_favourite.Equals("true"))
                    {
                        KnowledgeBaseModel kb = new KnowledgeBaseModel();
                        kb.id = fevkb.id;
                        fevKnowledgeList.Add(kb);
                    }
                }
            }
            bool ShouldPostData = false;
            FevouriteRequest fevRequest = new FevouriteRequest();
            if (fevMeditationList.Count > 0)
            {
                fevRequest.meditations = fevMeditationList;
                ShouldPostData = true;
            } 
            if (fevKnowledgeList.Count > 0)
            {
                fevRequest.knowledge = fevKnowledgeList;
                ShouldPostData = true;
            }

            if (ShouldPostData)
            {
                //if (String.IsNullOrEmpty(Settings.FevouriteId))
                //{
                //    fevRequest.id = Guid.NewGuid().ToString();
                //}
                //else
                //{
                //    fevRequest.id = Settings.FevouriteId;
                //}
                fevRequest.id = Settings.FevouriteId;
                fevRequest.mobile_user_id = Settings.MobileUserId;
                var fevService = new FevouriteService();
                FevouriteRequest fevResponse  = await fevService.UploadFevouriteList(fevRequest);
                //Settings.FevouriteId = fevResponse.id;
            }
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
