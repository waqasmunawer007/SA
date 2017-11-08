using Newtonsoft.Json;
using Services.Models.Meditation;
using SpirAtheneum.Constants;
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
        public async Task<MeditationModel[]> FetchAllMeditationAsync()
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

        public async Task<MeditationModel> FetchMeditationBaseOnIdAsync(string id)
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

        public async Task<string> UpdateFavourites(Dictionary<string, object> parameters) //todo
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                HttpResponseMessage responseJson = await client.PostAsync(APIsConstant.UpdateFavourites, content);
                var json = await responseJson.Content.ReadAsStringAsync();
                if (json != null)
                {
                    return "true";
                }
                else if (json == null)
                {
                    return "false";
                }
                //var empResponse = JsonConvert.DeserializeObject<string>(json);
                //return empResponse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
