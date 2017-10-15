using Services.Models.DailyDigest;
using Services.Models.Meditation;
using Services.Services.DailyDigest;
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
    class DailyDigestCategoryItemsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<DailyDigestModel> digetCategotyItems;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouritButtonCommand { get; set; }
        string selectedCategoryType;
        private bool isBusy = false;

        public DailyDigestCategoryItemsVM(string type) {
            digetCategotyItems = new ObservableCollection<DailyDigestModel>();
            selectedCategoryType = type;

            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouritButtonCommand = new Command((e) => {

                //todo
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
        public async Task<List<DailyDigestModel>> FetchAllDigestCategoryItems()
        {
            var dailydigestService = new DailyDigestService();
            DailyDigestModel[] allDigest = await dailydigestService.FetchAllDigestAsync();
            if (allDigest != null && allDigest.Length > 0) // extrect med items bases on selected category
            {
                List<DailyDigestModel> digestBasesOnSelectedDigestCategory = allDigest.Where(g => g.category == selectedCategoryType).ToList();
                return digestBasesOnSelectedDigestCategory;
            }
            else
            {
                Debug.WriteLine("Daily Digest list on Daily Digest item page is empty");
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
