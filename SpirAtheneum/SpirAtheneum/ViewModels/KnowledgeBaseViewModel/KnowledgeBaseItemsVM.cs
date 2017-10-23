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
        public ObservableCollection<KnowledgeBase> knowledgeBaseList;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }
        string selectedCategoryType;

        public KnowledgeBaseItemsVM(string type)
        {
            knowledgeBaseList = new ObservableCollection<KnowledgeBase>();
            databaseHelper = new DatabaseHelper();
            selectedCategoryType = type;

            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouriteButtonCommand = new Command((e) => {

                //todo
            });
        }

        public List<KnowledgeBase> FetchAllCategoryItems()
        {
            var allKnowledgeBase = databaseHelper.GetKnowledgeBase();
            if (allKnowledgeBase != null)
            {
                List<KnowledgeBase> knowledgeBasedOnSelectedCategory = allKnowledgeBase.Where(g => g.category == selectedCategoryType).ToList();
                return knowledgeBasedOnSelectedCategory;
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
