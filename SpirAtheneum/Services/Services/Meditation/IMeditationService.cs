using Services.Models.Meditation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Meditation
{
   public  interface IMeditationService
    {
        Task<MeditationModel[]> fetchAllMeditationAsync();
        Task<MeditationModel> fetchMeditationBaseOnIdAsync(string id);
        //Task<string> CreateMeditation(MeditationModel med);
        //Task<string> UpdateMeditation(MeditationModel med);
        //Task<string> DeleteMeditation(int id);




    }
}
