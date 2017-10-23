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
        KnowledgeBase item;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }

        public KnowledgeBaseItemDetailVM()
        {
            Item = new KnowledgeBase();
            databaseHelper = new DatabaseHelper();
            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouriteButtonCommand = new Command((e) => {

                //todo
            });
        }

        public KnowledgeBase Item
        {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
        }

        public KnowledgeBase FetchKnowledgeBaseItemDetail(string id)
        {
            var allKnowledgeBase = databaseHelper.GetKnowledgeBase();
            if (allKnowledgeBase != null)
            {
                KnowledgeBase itemDetail = allKnowledgeBase.First(x => x.id == id);
                return itemDetail;
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
