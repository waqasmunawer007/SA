using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Services.Models.Subscription;
using Services.Services.SubScription;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.Subscription
{
	public class SubscriptionViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<AppSubscription> subscriptionItems;
		
		public ICommand ShareButtonCommand { get; set; }
        private bool isBusy,isConatinerVisible = false;
        private string monthlySubscription = "";
        private string yearlySubscription = "";
        private string monthlyDesc = "";
        private string yearlyDesc = "";

		public SubscriptionViewModel()
		{
			subscriptionItems = new ObservableCollection<AppSubscription>();
			
			ShareButtonCommand = new Command((e) => {
			
			});
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
            AppSubscription[] subscriptionList = await subscriptionService.GetAppSubscriptionList();
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
