using SpirAtheneum.Database;
using SpirAtheneum.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.KnowledgeBaseViewModel
{
    class KnowledgeBaseItemDetailVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DatabaseHelper databaseHelper;
        KnowledgeBaseBinding item;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }

        public KnowledgeBaseItemDetailVM()
        {
            Item = new KnowledgeBaseBinding();
            databaseHelper = new DatabaseHelper();
            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouriteButtonCommand = new Command((e) => {
                var knowledge = (e as KnowledgeBaseBinding);
                databaseHelper.UpdateFavouriteKnowledgeBase(knowledge.id);
                if (knowledge.is_favourite == "true")
                {
                    knowledge.is_favourite = "false";
                }
                else if (knowledge.is_favourite == "false")
                {
                    knowledge.is_favourite = "true";
                }
                Item = knowledge;
            });
        }

        public KnowledgeBaseBinding Item
        {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
        }

        public KnowledgeBaseBinding FetchKnowledgeBaseItemDetail(string id)
        {
            var allKnowledgeBase = databaseHelper.GetKnowledgeBase();
            var allKnowledgeBaseFavourites = databaseHelper.GetKnowledgeBaseFavourite();

            if (allKnowledgeBase != null && allKnowledgeBaseFavourites != null)
            {
                KnowledgeBase itemDetail = allKnowledgeBase.First(x => x.id == id);
                FavouriteKnowledgeBase favourite = allKnowledgeBaseFavourites.First(x => x.id == id);

                KnowledgeBaseBinding kbb = new KnowledgeBaseBinding();

                kbb.id = itemDetail.id;
                kbb.title = itemDetail.title;
                kbb.text = itemDetail.text;
                kbb.category = itemDetail.category;
                kbb.is_favourite = favourite.is_favourite;

                return kbb;
            }
            else
            {
                Debug.WriteLine("KnowledgeBase item detail in KnowledgeBaseItemDetail page is empty");
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
