using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Models.AppActivity;
using SpirAtheneum.Constants;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Services.Services.AppActivity
{
    class AppActivityService : BaseService, IAppActivityService
    {
        public async Task<AppActivityModel[]> FetchAppActivityAsync()
        {
            try
            {
                string r = client.BaseAddress + APIsConstant.AppActivity;
                var responseJson = await client.GetAsync(APIsConstant.AppActivity);
                string json = await responseJson.Content.ReadAsStringAsync();
                if (!json.Equals("[]")) //only parse json if it contains data
                {
                    var activity = JsonConvert.DeserializeObject<AppActivityModel[]>(json);
                    return activity;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AppActivityService", ex.Message);
            }

            return null;
        }
    }
}
