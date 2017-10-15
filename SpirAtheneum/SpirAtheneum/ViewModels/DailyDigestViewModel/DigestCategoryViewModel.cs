using Services.Models.DailyDigest;
using Services.Services.DailyDigest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpirAtheneum.ViewModels.MeditationViewModel.MeditationVM;

namespace SpirAtheneum.ViewModels.DailyDigestViewModel
{
    public class DigestCategoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Category> DigestCategories;   //Category class defined as a inner class in MeditationVM
        private bool isBusy = false;
        public DigestCategoryViewModel()
        {

            DigestCategories = new ObservableCollection<Category>();

        }
        public async Task<List<Category>> FetchAllCategoryCategory()
        {
            var digestService = new DailyDigestService();
            DailyDigestModel[] allDigest = await digestService.FetchAllDigestAsync();

            if (allDigest != null && allDigest.Length > 0)
            {                                  // extract digest's categories from all Digest list
                List<Category> list = AppUtils.CategoryUtil.GetDigestCategoriesDetail(allDigest);
                return list;
            }
            else
            {
                Debug.WriteLine("Category list on Digest's Category page is empty");
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
