using Plugin.Connectivity;
using Services.Models.KnowledgeBase;
using Services.Services.AppActivity;
using Services.Services.KnowledgeBase;
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

namespace SpirAtheneum.ViewModels.KnowledgeBaseViewModel
{
    class KnowledgeBaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Category> knowledgeBaseList;
        public DatabaseHelper databaseHelper;
        private bool isBusy = false;

        public KnowledgeBaseVM()
        {
            knowledgeBaseList = new ObservableCollection<Category>();
            databaseHelper = new DatabaseHelper();
        }

        /// <summary>
        /// This function checks that if network is available or not. if its available, it hits Knowledge api and store api's data into local db, if not available then it just get data from local db
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> DatabaseOperation()
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                var appActivityService = new AppActivityService();
                var activity = await appActivityService.FetchAppActivityAsync();

                if (Settings.KnowledgeBase_LastUpdate != activity.First().knowledge_lastupdated.last_updated)
                {
                    List<KnowledgeBaseModel> items = await FetchAllKnowledgeBaseData();
                    databaseHelper.AddKnowledgeBase(items);
                    Settings.KnowledgeBase_LastUpdate = activity.First().knowledge_lastupdated.last_updated;
                    return FetchAllKnowledgeBaseCategories();
                }
                else
                {
                    return FetchAllKnowledgeBaseCategories();
                }
            }
            else
            {
                return FetchAllKnowledgeBaseCategories();
            }
        }

        public async Task<List<KnowledgeBaseModel>> FetchAllKnowledgeBaseData()
        {
            var knowledgeBaseService = new KnowledgeBaseService();
            KnowledgeBaseModel[] allKnowledge = await knowledgeBaseService.FetchAllKnowledgeBaseAsync();
            if (allKnowledge != null && allKnowledge.Length > 0)
            {
                List<KnowledgeBaseModel> allKnowledgeData = allKnowledge.ToList();
                return allKnowledgeData;
            }
            else
            {
                Debug.WriteLine("KnowledgeBase data is empty on KnowledgeBase Category page");
                return null;

            }
        }

        public List<Category> FetchAllKnowledgeBaseCategories()
        {
            var allKnowledgeBase = databaseHelper.GetKnowledgeBase();
            if (allKnowledgeBase != null)
            {
                List<Category> list = AppUtils.CategoryUtil.GetCountKnowledgeBase(allKnowledgeBase);
                return list;
            }
            else
            {
                Debug.WriteLine("KnowledgeBase list in category page is empty");
                return null;
            }
        }

        public bool IsBusy
        {
            get { return isBusy;  }
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
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
