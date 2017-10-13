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
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.MeditationViewModel
{
    class MeditationItmesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MeditationModel> meditaions;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouritButtonCommand { get; set; }
        string selectedCategoryType;
        private bool isBusy = false;

        public MeditationItmesVM(string type)
        {
            meditaions = new ObservableCollection<MeditationModel>();
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
        public async Task<List<MeditationModel>> FetchAllMeditationCategoryItems()
        {
            var meditationService = new MeditationService();
            MeditationModel[] allMeditation =   await meditationService.fetchAllMeditationAsync();
            if (allMeditation != null && allMeditation.Length > 0) // extrect med items bases on selected category
            {
                List<MeditationModel> medBasesOnSelectedMedCategory = allMeditation.Where(g => g.category == selectedCategoryType).ToList();
                return medBasesOnSelectedMedCategory;
            }
            else
            {
                Debug.WriteLine("Meditation list in Category item page in empty");
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
