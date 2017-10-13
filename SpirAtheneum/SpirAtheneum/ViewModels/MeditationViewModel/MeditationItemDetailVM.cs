using Services.Models.Meditation;
using Services.Services.Meditation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.MeditationViewModel
{
    class MeditationItemDetailVM : INotifyPropertyChanged
    {
        private bool isBusy = false;
        public event PropertyChangedEventHandler PropertyChanged;
        MeditationModel medItem;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouritButtonCommand { get; set; }
        public MeditationItemDetailVM()
        {
             medItem = new MeditationModel();
            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouritButtonCommand = new Command((e) => {

                //todo
            });
        }

        public MeditationModel MedItem
         {
            get { return medItem; }
            set
            {
                medItem = value;
                OnPropertyChanged("MedItem");
            }
         }

        public async Task<MeditationModel> FetchAllMeditationDetailItem(string id)
        {
            var meditationService = new MeditationService();
            MeditationModel med = await meditationService.fetchMeditationBaseOnIdAsync(id);
            if (med != null) 
             {
                return med;
             }
            else
             {
                Debug.WriteLine("Meditaion empty return");
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
