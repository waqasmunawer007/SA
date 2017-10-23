using Services.Models.Meditation;
using Services.Services.Meditation;
using SpirAtheneum.Database;
using SpirAtheneum.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.MeditationViewModel
{
    class MeditationItemsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DatabaseHelper databaseHelper;
        public ObservableCollection<Meditation> meditationList;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }
        string selectedCategoryType;

        public MeditationItemsVM(string type)
        {
            meditationList = new ObservableCollection<Meditation>();
            databaseHelper = new DatabaseHelper();
            selectedCategoryType = type;

            ShareButtonCommand = new Command((e) => {
              
               //todo
            });
            FavouriteButtonCommand = new Command((e) => {

                //todo
            });
        }

        public List<Meditation> FetchAllCategoryItems()
        {
            var allMeditation = databaseHelper.GetMeditation();
            if (allMeditation != null)
            {
                List<Meditation> meditationBasedOnSelectedCategory = allMeditation.Where(g => g.category == selectedCategoryType).ToList();
                return meditationBasedOnSelectedCategory;
            }
            else
            {
                Debug.WriteLine("Meditation list in Category item page is empty");
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
