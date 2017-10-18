using Services.Models.KnowledgeBase;
using Services.Services.KnowledgeBase;
using SpirAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.ViewModels.KnowledgeBaseViewModel
{
    public class KnowledgeBaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Category> knowledgeBaseList;
        private bool isBusy = false;

        public KnowledgeBaseVM()
        {
            knowledgeBaseList = new ObservableCollection<Category>();
        }

        public async Task<List<Category>> FetchAllKnowledgeBaseCategory()
        {
            var knowledgeBaseService = new KnowledgeBaseService();
            KnowledgeBaseModel[] allKnowledgeBase = await knowledgeBaseService.FetchAllKnowledgeBaseAsync();

            if(allKnowledgeBase != null && allKnowledgeBase.Length > 0) // extract unique meditation categories from all meditattion list
            {
                List<Category> list = AppUtils.CategoryUtil.GetCountKnowledgeBase(allKnowledgeBase);
                return list;
            }
            else
            {
                Debug.WriteLine("KnowledgeBase list in category page is empty");
                return null;
            }
        }

        public bool IsBusy
        {
            get { return isBusy;  }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
