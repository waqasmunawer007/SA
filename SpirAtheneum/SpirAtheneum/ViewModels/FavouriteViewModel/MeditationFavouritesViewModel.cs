using SpirAtheneum.Database;
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

namespace SpirAtheneum.ViewModels.FavouriteViewModel
{
    class MeditationFavouritesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DatabaseHelper databaseHelper;
        public ObservableCollection<MeditationBinding> meditationList;
        public ICommand FavouriteButtonCommand { get; set; }

        public ObservableCollection<MeditationBinding> MeditationBinding
        {
            get { return meditationList; }
            set
            {
                if (meditationList != value)
                {
                    meditationList = value;
                    OnPropertyChanged("MeditationBinding");
                }
            }
        }

        public MeditationFavouritesViewModel()
        {
            meditationList = new ObservableCollection<MeditationBinding>();
            databaseHelper = DatabaseHelper.GetInstance();

            FavouriteButtonCommand = new Command((e) => {
                var meditation = (e as MeditationBinding);
                databaseHelper.UpdateFavouriteMeditation(meditation.id);
                if (meditation.is_favourite == "true")
                {
                    meditation.is_favourite = "false";
                }
                for (int i = 0; i < MeditationBinding.Count; i++)
                {
                    if (MeditationBinding[i].id == meditation.id)
                    {
                        MeditationBinding.Remove(MeditationBinding[i]);
                        break;
                    }
                }
            });
        }

        public List<MeditationBinding> FetchAllFavourites()
        {
            var allMeditations = databaseHelper.GetMeditation();
            var allMeditationFavourites = databaseHelper.GetMeditationFavourite();

            if(allMeditations != null && allMeditationFavourites != null)
            {
                try
                {
                    var allFavourites = allMeditationFavourites.Where(x => x.is_favourite == "true").ToList();
                    List<MeditationBinding> combinedList = new List<MeditationBinding>();
                    foreach(var f in allFavourites)
                    {
                        var meditation = allMeditations.Find(x => x.id == f.id);

                        MeditationBinding mb = new MeditationBinding();

                        mb.id = meditation.id;
                        mb.html_string = meditation.html_string;
                        mb.title = meditation.title;
                        mb.category = meditation.category;
                        mb.is_favourite = f.is_favourite;

                        combinedList.Add(mb);
                    }

                    return combinedList;
                }
                catch
                {
                    Debug.WriteLine("No favourites found!");
                    return null;
                }
            }
            else
            {
                Debug.WriteLine("List in MeditationFavouritesPage is empty");
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
