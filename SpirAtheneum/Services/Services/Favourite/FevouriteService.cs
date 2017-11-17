using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Models.Favourite;
using SpirAtheneum.Constants;

namespace Services.Services.Favourite
{
    public class FevouriteService:BaseService,IFevourite
    {
      

        public async Task<FevouriteRequest> UploadFevouriteList(FevouriteRequest requestParameters)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(requestParameters), Encoding.UTF8, "application/json");
                HttpResponseMessage responseJson = await client.PostAsync(APIsConstant.UploadFevApi, content);
                var json = await responseJson.Content.ReadAsStringAsync();
                if (json != null) //only parse json if it contains data
                {
                    var fevResponse = JsonConvert.DeserializeObject<FevouriteRequest>(json);
                    return fevResponse;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MobileUser", ex.Message);
            }

            return null; 
           
        }
    }
}
