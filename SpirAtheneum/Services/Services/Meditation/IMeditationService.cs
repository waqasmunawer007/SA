using Services.Models.Meditation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Meditation
{
   public  interface IMeditationService
    {
        Task<MeditationModel[]> FetchAllMeditationAsync();
        Task<MeditationModel> FetchMeditationBaseOnIdAsync(string id);
        Task<string> UpdateFavourites(Dictionary<string, object> parameters);
    }
}
