using Services.Models.KnowledgeBase;
using Services.Services.KnowledgeBase;
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

namespace SpirAtheneum.ViewModels.KnowledgeBaseViewModel
{
    class KnowledgeBaseItemsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<KnowledgeBaseModel> knowledgeBaseList;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }
        string selectedCategoryType;
        private bool isBusy = false;

        public KnowledgeBaseItemsVM(string type)
        {
            knowledgeBaseList = new ObservableCollection<KnowledgeBaseModel>();
            selectedCategoryType = type;

            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouriteButtonCommand = new Command((e) => {

                //todo
            });

        }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        public async Task<List<KnowledgeBaseModel>> FetchAllCategoryItems()
        {
            var knowledgeBaseService = new KnowledgeBaseService();
            KnowledgeBaseModel[] allKnowledgeBase = await knowledgeBaseService.FetchAllKnowledgeBaseAsync();
            if (allKnowledgeBase != null && allKnowledgeBase.Length > 0) // extrect knowledgebase items bases on selected category
            {
                List<KnowledgeBaseModel> knowledgeBasedOnSelectedCategory = allKnowledgeBase.Where(g => g.category == selectedCategoryType).ToList();
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
