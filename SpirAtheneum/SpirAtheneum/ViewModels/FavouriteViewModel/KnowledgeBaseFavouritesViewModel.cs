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
    class KnowledgeBaseFavouritesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DatabaseHelper databaseHelper;
        public ObservableCollection<KnowledgeBaseBinding> knowledgeList;
        public ICommand FavouriteButtonCommand { get; set; }

        public ObservableCollection<KnowledgeBaseBinding> KnowledgeBaseBinding
        {
            get { return knowledgeList; }
            set
            {
                if (knowledgeList != value)
                {
                    knowledgeList = value;
                    OnPropertyChanged("KnowledgeBaseBinding");
                }
            }
        }

        public KnowledgeBaseFavouritesViewModel()
        {
            knowledgeList = new ObservableCollection<KnowledgeBaseBinding>();
            databaseHelper = new DatabaseHelper();

            FavouriteButtonCommand = new Command((e) => {
                var knowledge = (e as KnowledgeBaseBinding);
                databaseHelper.UpdateFavouriteKnowledgeBase(knowledge.id);
                if (knowledge.is_favourite == "true")
                {
                    knowledge.is_favourite = "false";
                }
                for (int i = 0; i < KnowledgeBaseBinding.Count; i++)
                {
                    if (KnowledgeBaseBinding[i].id == knowledge.id)
                    {
                        KnowledgeBaseBinding.Remove(KnowledgeBaseBinding[i]);
                        break;
                    }
                }
            });
        }

        public List<KnowledgeBaseBinding> FetchAllFavourites()
        {
            var allKnowledge = databaseHelper.GetKnowledgeBase();
            var allKnowledgeFavourites = databaseHelper.GetKnowledgeBaseFavourite();

            if (allKnowledge != null && allKnowledgeFavourites != null)
            {
                try
                {
                    var allFavourites = allKnowledgeFavourites.Where(x => x.is_favourite == "true").ToList();
                    List<KnowledgeBaseBinding> combinedList = new List<KnowledgeBaseBinding>();
                    foreach (var f in allFavourites)
                    {
                        var knowledge = allKnowledge.Find(x => x.id == f.id);

                        KnowledgeBaseBinding kbb = new KnowledgeBaseBinding();

                        kbb.id = knowledge.id;
                        kbb.title = knowledge.title;
                        kbb.text = knowledge.text;
                        kbb.category = knowledge.category;
                        kbb.is_favourite = f.is_favourite;

                        combinedList.Add(kbb);
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
                Debug.WriteLine("List in KnowledgeBaseFavourites page is empty");
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
