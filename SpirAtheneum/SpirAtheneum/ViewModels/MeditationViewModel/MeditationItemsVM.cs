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
        public ObservableCollection<MeditationBinding> meditationList;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }
        string selectedCategoryType;

        public ObservableCollection<MeditationBinding> MeditationBinding
        {
            get{ return meditationList; }
            set
            {
                if (meditationList != value)
                {
                    meditationList = value;
                    OnPropertyChanged("MeditationBinding");
                }
            }
        }

        public MeditationItemsVM(string type)
        {
            meditationList = new ObservableCollection<MeditationBinding>();
            databaseHelper = new DatabaseHelper();
            selectedCategoryType = type;

            ShareButtonCommand = new Command((e) => {
                
               //todo
            });
            FavouriteButtonCommand = new Command((e) => {
                var meditation = (e as MeditationBinding);
                databaseHelper.UpdateFavouriteMeditation(meditation.id);
                if (meditation.is_favourite == "true")
                {
                    meditation.is_favourite = "false";
                }
                else if (meditation.is_favourite == "false")
                {
                    meditation.is_favourite = "true";
                }
                for(int i = 0; i < MeditationBinding.Count; i++)
                {
                    if(MeditationBinding[i].id == meditation.id)
                    {
                        MeditationBinding[i] = meditation;
                        break;
                    }
                }
            });
        }

        public List<MeditationBinding> FetchAllCategoryItems()
        {
            var allMeditations = databaseHelper.GetMeditation();
            var allMeditationFavourites = databaseHelper.GetMeditationFavourite();

            if (allMeditations != null && allMeditationFavourites != null)
            {
                List<Meditation> meditationBasedOnSelectedCategory = allMeditations.Where(g => g.category == selectedCategoryType).ToList();
                List<MeditationBinding> combinedList = new List<MeditationBinding>();

                foreach (var meditation in meditationBasedOnSelectedCategory)
                {
                    var favourite = allMeditationFavourites.Find(x => x.id == meditation.id);

                    MeditationBinding mb = new MeditationBinding();

                    mb.id = meditation.id;
                    mb.intro = meditation.intro;
                    mb.outro = meditation.outro;
                    mb.title = meditation.title;
                    mb.category = meditation.category;
                    mb.is_favourite = favourite.is_favourite;

                    combinedList.Add(mb);
                }

                return combinedList;
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
