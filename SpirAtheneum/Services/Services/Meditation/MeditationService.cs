using Newtonsoft.Json;
using Services.Models.Meditation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Meditation
{
    class MeditationService : BaseService, IMeditationService
    {
        public async Task<MeditationModel[]> fetchAllMeditationAsync()
        {
            try
            {
               
                 var responseJson = await client.GetAsync(SpirAtheneum.Constants.APIsConstant.AllMeditation);
                string json = await responseJson.Content.ReadAsStringAsync();
                if (!json.Equals("[]")) //only parse json if it contains data
                {
                    var meditationList = JsonConvert.DeserializeObject<MeditationModel[]>(json);
                    return meditationList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FetchAllMeditation", ex.Message);
            }

            return null;
        }

        public async Task<MeditationModel> fetchMeditationBaseOnIdAsync(string id)
        {
            try
            {
                string r = client.BaseAddress + SpirAtheneum.Constants.APIsConstant.AllMeditation+"?id="+id;
                var responseJson = await client.GetAsync(r);
                string json = await responseJson.Content.ReadAsStringAsync();
                if (!json.Equals(null)) //only parse json if it contains data
                {
                    var meditation = JsonConvert.DeserializeObject<MeditationModel>(json);
                    return meditation;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FetchMeditationBaseOn Id", ex.Message);
            }

            return null;
        }
    }
}
