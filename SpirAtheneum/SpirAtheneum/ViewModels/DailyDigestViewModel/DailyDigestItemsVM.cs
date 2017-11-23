using Plugin.Connectivity;
using Services.Models.DailyDigest;
using Services.Models.Meditation;
using Services.Services.AppActivity;
using Services.Services.DailyDigest;
using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using SpirAtheneum.Interfaces;
using SpirAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.DailyDigestViewModel
{
    class DailyDigestItemsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<DailyDigest> dailyDigestItems;
        public DatabaseHelper databaseHelper;
        public ICommand ShareButtonCommand { get; set; }
        private bool isBusy = false;

        public DailyDigestItemsVM()
        {
            dailyDigestItems = new ObservableCollection<DailyDigest>();
            databaseHelper = DatabaseHelper.GetInstance();

            ShareButtonCommand = new Command((e) => {
                var a = (e as DailyDigest);
                var share = DependencyService.Get<IShare>();
                share.Show(a.text, a.text);
            });
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

        /// <summary>
        /// This function checks that if network is available or not. if its available, it hits DailyDigest api and store api's data into local db, if not available then it just get data from local db
        /// </summary>
        /// <returns></returns>
        public async Task<List<DailyDigest>> DatabaseOperation()
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                var appActivityService = new AppActivityService();
                var activity =  await appActivityService.FetchAppActivityAsync();

                LastUpdateDailyDigest lastUpdateDailyDigestDate = DatabaseHelper.GetInstance().GetLastDailyDigestDate();
                if (lastUpdateDailyDigestDate == null || lastUpdateDailyDigestDate.DailyDigestLastUpdateDate != activity.First().digest_activity.last_updated)
                {
                    List<DailyDigestModel> items = await FetchAllDigestItems();
                    databaseHelper.AddDailyDigest(items);
                    var dailyDigestLocalData = databaseHelper.GetDailyDigest();
                    DatabaseHelper.GetInstance().SaveLastUpdateDailyDigestDate(activity.First().digest_activity.last_updated);
                    return dailyDigestLocalData;

                }
                else
                {
                    var dailyDigestLocalData = databaseHelper.GetDailyDigest();
                    if (dailyDigestLocalData != null)
                    {
                        return dailyDigestLocalData;
                    }
                    else
                    {
                        Debug.WriteLine("DailyDigest local data is empty on DailyDigestItemPage");
                        return null;
                    }
                }
                //if (Settings.DailyDigest_LastUpdate != activity.First().digest_activity.last_updated)
                //{
                //    List<DailyDigestModel> items = await FetchAllDigestItems();
                //    databaseHelper.AddDailyDigest(items);
                //    var dailyDigestLocalData = databaseHelper.GetDailyDigest();
                //    Settings.DailyDigest_LastUpdate = activity.First().digest_activity.last_updated;
                //    return dailyDigestLocalData;
                //}
                //else
                //{
                //    var dailyDigestLocalData = databaseHelper.GetDailyDigest();
                //    if (dailyDigestLocalData != null)
                //    {
                //        return dailyDigestLocalData;
                //    }
                //    else
                //    {
                //        Debug.WriteLine("DailyDigest local data is empty on DailyDigestItemPage");
                //        return null;
                //    }
                //}

            }
            else
            {
                var dailyDigestLocalData = databaseHelper.GetDailyDigest();
                if (dailyDigestLocalData != null)
                {
                    return dailyDigestLocalData;
                }
                else
                {
                    Debug.WriteLine("DailyDigest local data is empty on DailyDigestItemPage");
                    return null;
                }
            }
        }

        public async Task<List<DailyDigestModel>> FetchAllDigestItems()
        {
            var dailydigestService = new DailyDigestService();
            DailyDigestModel[] allDigest = await dailydigestService.FetchAllDigestAsync();
            if (allDigest != null && allDigest.Length > 0)
            {
                List<DailyDigestModel> allDigestItems = allDigest.ToList();
                List<DailyDigestModel> digestBasedOnPublishedTime = new List<DailyDigestModel>();

                foreach(var digest in allDigestItems)
                {
                    if(DateTime.Now >= digest.publish_date)
                    {
                        digestBasedOnPublishedTime.Add(digest);
                    }
                }
                var dailyDigestSorted = digestBasedOnPublishedTime.OrderByDescending(x => x.publish_date).ToList();
                return dailyDigestSorted;
            }
            else
            {
                Debug.WriteLine("DailyDigest list on DailyDigestItemPage is empty");
                return null;

            }
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
