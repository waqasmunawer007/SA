using Newtonsoft.Json;
using SpirAtheneum.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Signup
{
    public class SignupService : BaseService, ISignupService
    {
        public async Task<string> Signup(Dictionary<string, object> parameters) //todo
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                HttpResponseMessage responseJson = await client.PostAsync(APIsConstant.Signup, content);
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
