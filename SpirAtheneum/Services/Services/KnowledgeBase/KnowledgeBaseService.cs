using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Models.KnowledgeBase;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Services.Services.KnowledgeBase
{
    class KnowledgeBaseService : BaseService, IKnowledgeBaseService
    {
        public async Task<KnowledgeBaseModel[]> FetchAllKnowledgeBaseAsync()
        {
            try
            {
                var responseJson = await client.GetAsync(SpirAtheneum.Constants.APIsConstant.AllKnowledgeBase);
                string json = await responseJson.Content.ReadAsStringAsync();
                if(!json.Equals("[]")) //only parse json if it contains data
                {
                    var knowledgeBaseList = JsonConvert.DeserializeObject<KnowledgeBaseModel[]>(json);
                    return knowledgeBaseList;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("fetchAllKnowledgeBase", ex.Message);
            }

            return null;
        }

        public async Task<KnowledgeBaseModel> FetchKnowledgeBaseUsingIdAsync(string id)
        {
            try
            {
                string r = client.BaseAddress + SpirAtheneum.Constants.APIsConstant.AllKnowledgeBase + "?id=" + id;
                var responseJson = await client.GetAsync(r);
                string json = await responseJson.Content.ReadAsStringAsync();
                if(!json.Equals(null))
                {
                    var knowledgeBase = JsonConvert.DeserializeObject<KnowledgeBaseModel>(json);
                    return knowledgeBase;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("fetchKnowledgeBaseUsingId", ex.Message);
            }

            return null;
        }
    }
}
