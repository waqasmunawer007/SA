using Services.Models.KnowledgeBase;
using Services.Services.KnowledgeBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.KnowledgeBaseViewModel
{
    class KnowledgeBaseItemDetailVM : INotifyPropertyChanged
    {
        private bool isBusy = false;
        public event PropertyChangedEventHandler PropertyChanged;
        KnowledgeBaseModel item;
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouriteButtonCommand { get; set; }
        public KnowledgeBaseItemDetailVM()
        {
            Item = new KnowledgeBaseModel();
            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouriteButtonCommand = new Command((e) => {

                //todo
            });
        }

        public KnowledgeBaseModel Item
        {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
        }

        public async Task<KnowledgeBaseModel> FetchKnowledgeBaseItemDetail(string id)
        {
            var knowledgeBaseService = new KnowledgeBaseService();
            KnowledgeBaseModel knowledge = await knowledgeBaseService.FetchKnowledgeBaseUsingIdAsync(id);
            if (knowledge != null)
            {
                return knowledge;
            }
            else
            {
                Debug.WriteLine("Knowledge empty return");
                return null;
            }
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
