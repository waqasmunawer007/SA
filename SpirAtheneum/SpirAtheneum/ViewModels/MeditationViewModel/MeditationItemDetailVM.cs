using Services.Models.Meditation;
using Services.Services.Meditation;
using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using SpirAtheneum.Interfaces;
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
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }

        public MeditationItemDetailVM()
        {
            item = new MeditationBinding();
            databaseHelper = new DatabaseHelper();

            ShareButtonCommand = new Command((e) => {
                var a = (e as MeditationBinding);

                HtmlParser htmlParser = new HtmlParser();

                var text = htmlParser.GetFormattedTextFromHtml(a.html_string);
                var shareMessage = a.title + ":\n" + a.share_message;

                var share = DependencyService.Get<IShare>();
                share.Show(text, shareMessage);
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

        public MeditationBinding FetchMeditationItemDetail(string id)
        {
            var allMeditation = databaseHelper.GetMeditation();
            var allMeditationFavourites = databaseHelper.GetMeditationFavourite();

            if (allMeditation != null && allMeditationFavourites != null)
            {
                Meditation itemDetail = allMeditation.First(x => x.id == id);
                FavouriteMeditation favourite = allMeditationFavourites.First(x => x.id == id);

                MeditationBinding mb = new MeditationBinding();

                mb.id = itemDetail.id;
                mb.html_string = itemDetail.html_string;
                mb.title = itemDetail.title;
                mb.category = itemDetail.category;
                mb.share_message = itemDetail.share_message;
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
