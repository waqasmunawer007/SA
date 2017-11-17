using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Models.Subscription;
using SpirAtheneum.Constants;

namespace Services.Services.SubScription
{
    public class SubscriptionService:BaseService,ISubscription
    {
       
        public async Task<AppSubscription[]> GetAppSubscriptionList()
        {
			try
			{
				var responseJson = await client.GetAsync(APIsConstant.AppSubscription);
				string json = await responseJson.Content.ReadAsStringAsync();
				if (!json.Equals("[]")) //only parse json if it contains data
				{
					var subscriptions = JsonConvert.DeserializeObject<AppSubscription[]>(json);
					return subscriptions;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("FetchAppSubscriptions", ex.Message);
			}

			return null;
        }
		
    }
}
