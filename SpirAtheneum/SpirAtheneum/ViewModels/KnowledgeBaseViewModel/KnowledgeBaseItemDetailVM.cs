using SpirAtheneum.Database;
using SpirAtheneum.Helpers;
using SpirAtheneum.Models;
using SpirAtheneum.Interfaces;
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
                var a = (e as KnowledgeBaseBinding);
                HtmlParser htmlParser = new HtmlParser();

                var text = a.title + ":\n" + htmlParser.GetFormattedTextFromHtml(a.text);
                var shareMessage = a.title + ":\n" + a.share_message;

                var share = DependencyService.Get<IShare>();
                share.Show(text, shareMessage);
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
                kbb.share_message = itemDetail.share_message;
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
