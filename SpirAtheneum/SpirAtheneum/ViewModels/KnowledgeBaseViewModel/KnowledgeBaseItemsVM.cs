using SpirAtheneum.Database;
using SpirAtheneum.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.KnowledgeBaseViewModel
{
    class KnowledgeBaseItemsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DatabaseHelper databaseHelper;
        public ObservableCollection<KnowledgeBaseBinding> knowledgeBaseList;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }
        string selectedCategoryType;

        public ObservableCollection<KnowledgeBaseBinding> KnowledgeBaseBinding
        {
            get { return knowledgeBaseList; }
            set
            {
                if (knowledgeBaseList != value)
                {
                    knowledgeBaseList = value;
                    OnPropertyChanged("KnowledgeBaseBinding");
                }
            }
        }

        public KnowledgeBaseItemsVM(string type)
        {
            knowledgeBaseList = new ObservableCollection<KnowledgeBaseBinding>();
            databaseHelper = new DatabaseHelper();
            selectedCategoryType = type;

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
                for (int i = 0; i < KnowledgeBaseBinding.Count; i++)
                {
                    if (KnowledgeBaseBinding[i].id == knowledge.id)
                    {
                        KnowledgeBaseBinding[i] = knowledge;
                        break;
                    }
                }
            });
        }

        public List<KnowledgeBaseBinding> FetchAllCategoryItems()
        {
            var allKnowledgeBase = databaseHelper.GetKnowledgeBase();
            var allKnowledgeBaseFavourites = databaseHelper.GetKnowledgeBaseFavourite();

            if (allKnowledgeBase != null && allKnowledgeBaseFavourites != null)
            {
                List<KnowledgeBase> knowledgeBasedOnSelectedCategory = allKnowledgeBase.Where(g => g.category == selectedCategoryType).ToList();
                List<KnowledgeBaseBinding> combinedList = new List<KnowledgeBaseBinding>();

                foreach (var knowledge in knowledgeBasedOnSelectedCategory)
                {
                    var favourite = allKnowledgeBaseFavourites.Find(x => x.id == knowledge.id);

                    KnowledgeBaseBinding kbb = new KnowledgeBaseBinding();

                    kbb.id = knowledge.id;
                    kbb.title = knowledge.title;
                    kbb.text = knowledge.text;
                    kbb.category = knowledge.category;
                    kbb.is_favourite = favourite.is_favourite;

                    combinedList.Add(kbb);
                }

                return combinedList;
            }
            else
            {
                Debug.WriteLine("KnowledgeBase list in Category item page is empty");
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
