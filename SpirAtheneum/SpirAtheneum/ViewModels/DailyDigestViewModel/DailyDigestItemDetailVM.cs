using Services.Models.DailyDigest;
using Services.Models.Meditation;
using Services.Services.DailyDigest;
using Services.Services.Meditation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SpirAtheneum.ViewModels.DailyDigestViewModel
{
    class DailyDigestItemDetailVM : INotifyPropertyChanged
    {
        private bool isBusy = false;
        public event PropertyChangedEventHandler PropertyChanged;
        private DailyDigestModel digestItem;
        
        public ICommand ShareButtonCommand { get; set; }
        public ICommand FavouritButtonCommand { get; set; }
        public DailyDigestItemDetailVM()
        {
            digestItem = new DailyDigestModel();
            ShareButtonCommand = new Command((e) => {

                //todo
            });
            FavouritButtonCommand = new Command((e) => {

                //todo
            });
        }

        public DailyDigestModel DigestItem
         {
            get { return digestItem; }
            set
            {
                digestItem = value;
                OnPropertyChanged("DigestItem");
            }
         }

        public async Task<DailyDigestModel> FetchDIgestItemDetail(string id)
        {
            var dailyDigestService = new DailyDigestService();
            DailyDigestModel item = await dailyDigestService.FetchDigestBaseOnIdAsync(id);
            if (item != null) 
             {
                return item;
             }
            else
             {
                Debug.WriteLine("empty item return");
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
