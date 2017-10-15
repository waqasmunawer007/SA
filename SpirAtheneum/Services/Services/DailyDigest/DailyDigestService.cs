using Services.Services.DailyDigestService;
using System;
using System.Collections.Generic;
using System.Text;
using Services.Models.DailyDigest;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Services.Services.DailyDigest
{
    class DailyDigestService : BaseService, IDailyDigestService
    {
        public async System.Threading.Tasks.Task<DailyDigestModel[]> FetchAllDigestAsync()
        {
            try
            {
                string r = client.BaseAddress + SpirAtheneum.Constants.APIsConstant.AllMeditation;
                var responseJson = await client.GetAsync(SpirAtheneum.Constants.APIsConstant.DigestAPi);
                string json = await responseJson.Content.ReadAsStringAsync();
                if (!json.Equals("[]")) //only parse json if it contains data
                {
                    var meditationList = JsonConvert.DeserializeObject<DailyDigestModel[]>(json);
                    return meditationList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FetchAllDigestService", ex.Message);
            }

            return null;
        }

        public async System.Threading.Tasks.Task<DailyDigestModel> FetchDigestBaseOnIdAsync(string id)
        {
            try
            {
                string r = client.BaseAddress + SpirAtheneum.Constants.APIsConstant.DigestAPi + "?id=" + id;
                var responseJson = await client.GetAsync(r);
                string json = await responseJson.Content.ReadAsStringAsync();
                if (!json.Equals(null)) //only parse json if it contains data
                {
                    var meditation = JsonConvert.DeserializeObject<DailyDigestModel>(json);
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
