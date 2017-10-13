using Services.Models.Meditation;
using Services.Services.Meditation;
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
   public class MeditationVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Category> meditaions;
        private bool isBusy = false;
        public MeditationVM() {

            meditaions = new ObservableCollection<Category>();
            
        }
        public async Task<List<Category>> FetchAllMeditationCategory()
        {
            var meditationService = new MeditationService();
            MeditationModel[] allMeditation = await meditationService.fetchAllMeditationAsync();

            if (allMeditation != null && allMeditation.Length > 0)  // extract unique meditation categories from all meditattion list
            {
                List<Category> list = AppUtils.CategoryUtil.GetCount(allMeditation);
                return list;
            }
            else {
                Debug.WriteLine("Meditation list in Category page in empty");
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

        public class Category
        {
            public int count { get; set; }
            public string title { get; set; }
            public string category { get; set; }

        }

    }
}
