using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Models.MobileUser;
using SpirAtheneum.Constants;

namespace Services.Services.MobileUser
{
    public class MobileUserService : BaseService, IMobileUser
    {
        public async Task<AppMobileUser> CreateMobileUser(AppMobileUser mobileUser)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(mobileUser), Encoding.UTF8, "application/json");
                HttpResponseMessage responseJson = await client.PostAsync(APIsConstant.MobileUser, content);
                var json = await responseJson.Content.ReadAsStringAsync();
                if (json != null) //only parse json if it contains data
                {
                    var user = JsonConvert.DeserializeObject<AppMobileUser>(json);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MobileUser", ex.Message);
            }

            return null; 
        }

        public async Task<bool> IsMobileUserAlreadyExsit(AppMobileUser mobileUser)
        {
            try
            {
                var responseJson = await client.GetAsync(APIsConstant.CheckMobileUser+"?email="+mobileUser.email);
                var json = await responseJson.Content.ReadAsStringAsync();
                if (!json.Equals("[]")) //only parse json if it contains data
                {
                    string[] emails = JsonConvert.DeserializeObject<string[]>(json);
                    foreach (string email in emails)
                    {
                        if (email.ToLower().Equals(mobileUser.email.ToLower()))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MobileUser", ex.Message);
            }
            return false; 
        }

        public async Task<AppMobileUser> UpdateMobileUser(AppMobileUser mobileUser)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(mobileUser), Encoding.UTF8, "application/json");
                HttpResponseMessage responseJson = await client.PutAsync(APIsConstant.MobileUser, content);
                var json = await responseJson.Content.ReadAsStringAsync();
                if (json != null) //only parse json if it contains data
                {
                    var user = JsonConvert.DeserializeObject<AppMobileUser>(json);
                    return user;
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
