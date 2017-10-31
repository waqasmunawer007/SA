using Plugin.Connectivity;
using Services.Models.Meditation;
using Services.Services.AppActivity;
using Services.Services.Meditation;
using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using SpirAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.ViewModels.MeditationViewModel
{
    class MeditationVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Category> meditationList;
        public DatabaseHelper databaseHelper;
        private bool isBusy = false;

        public MeditationVM()
        {
            meditationList = new ObservableCollection<Category>();
            databaseHelper = new DatabaseHelper();
        }

        /// <summary>
        /// This function checks that if network is available or not. if its available, it hits Meditation api and store api's data into local db, if not available then it just get data from local db
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> DatabaseOperation()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var appActivityService = new AppActivityService();
                var activity = await appActivityService.FetchAppActivityAsync();
                if (Settings.Meditation_LastUpdate != activity.First().meditation_activity.last_updated)
                {
                    List<MeditationModel> items = await FetchAllMeditationData();
                    databaseHelper.AddMeditation(items);
                    Settings.Meditation_LastUpdate = activity.First().meditation_activity.last_updated;
                    return FetchAllMeditationCategories();
                }
                else
                {
                    return FetchAllMeditationCategories();
                }
            }
            else
            {
                return FetchAllMeditationCategories();
            }
        }

        public async Task<List<MeditationModel>> FetchAllMeditationData()
        {
            var meditationService = new MeditationService();
            MeditationModel[] allMeditation = await meditationService.FetchAllMeditationAsync();
            if (allMeditation != null && allMeditation.Length > 0)
            {
                List<MeditationModel> allMeditationData = allMeditation.ToList();
                return allMeditationData;
            }
            else
            {
                Debug.WriteLine("Meditation data is empty on Meditation Category page");
                return null;
            }
        }

        public List<Category> FetchAllMeditationCategories()
        {
            var allMeditation = databaseHelper.GetMeditation();
            if (allMeditation != null)
            {
                List<Category> list = AppUtils.CategoryUtil.GetCountMeditation(allMeditation);
                return list;
            }
            else
            {
                Debug.WriteLine("Meditation list in category page is empty");
                return null;
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

        private void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

