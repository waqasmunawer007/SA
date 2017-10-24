using Services.Models.Meditation;
using Services.Services.Meditation;
using SpirAtheneum.Database;
using SpirAtheneum.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.MeditationViewModel
{
    class MeditationItemDetailVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DatabaseHelper databaseHelper;
        MeditationBinding item;
        //Step stepitems;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }

        public MeditationItemDetailVM()
        {
            item = new MeditationBinding();
            databaseHelper = new DatabaseHelper();

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
                Item = meditation;
            });
        }

        public MeditationBinding Item
         {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
         }

        //public Step Steps
        //{
        //    get { return stepitems; }
        //    set
        //    {
        //        stepitems = value;
        //        OnPropertyChanged("Steps");
        //    }
        //}


        public MeditationBinding FetchMeditationItemDetail(string id)
        {
            var allMeditation = databaseHelper.GetMeditation();
            var allMeditationFavourites = databaseHelper.GetMeditationFavourite();
            //var allSteps = databaseHelper.GetMeditationSteps();

            if (allMeditation != null && allMeditationFavourites != null)
            {
                Meditation itemDetail = allMeditation.First(x => x.id == id);
                FavouriteMeditation favourite = allMeditationFavourites.First(x => x.id == id);

                MeditationBinding mb = new MeditationBinding();

                mb.id = itemDetail.id;
                mb.intro = itemDetail.intro;
                mb.outro = itemDetail.outro;
                mb.title = itemDetail.title;
                mb.category = itemDetail.category;
                mb.is_favourite = favourite.is_favourite;

                return mb;
            }
            else
            {
                Debug.WriteLine("Meditation item detail in MeditationItemDetail page is empty");
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
