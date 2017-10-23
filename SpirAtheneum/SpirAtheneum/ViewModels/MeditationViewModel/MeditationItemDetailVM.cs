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
        Meditation item;
        Step stepitems;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }

        public MeditationItemDetailVM()
        {
            item = new Meditation();
            databaseHelper = new DatabaseHelper();

            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouriteButtonCommand = new Command((e) => {

                //todo
            });
        }

        public Meditation Item
         {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
         }

        public Step Steps
        {
            get { return stepitems; }
            set
            {
                stepitems = value;
                OnPropertyChanged("Steps");
            }
        }


        public Meditation FetchMeditationItemDetail(string id)
        {
            var allMeditation = databaseHelper.GetMeditation();
            var allSteps = databaseHelper.GetMeditationSteps();
            if (allMeditation != null)
            {
                Meditation itemDetail = allMeditation.First(x => x.id == id);
                return itemDetail;
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
