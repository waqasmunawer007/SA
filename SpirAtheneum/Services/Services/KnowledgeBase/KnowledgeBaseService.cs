using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Models.KnowledgeBase;

namespace Services.Services.KnowledgeBase
{
    class KnowledgeBaseService : BaseService, IKnowledgeBaseService
    {
        public Task<KnowledgeBaseModel[]> fetchAllKnowledgeBaseAsync()
        {
            throw new NotImplementedException();
            //try
            //{

            //    var responseJson = await client.GetAsync(SpirAtheneum.Constants.APIsConstant.AllKnowledgeBase);
            //    string json = await responseJson.Content.ReadAsStringAsync();
            //    if (!json.Equals("[]")) //only parse json if it contains data
            //    {
            //        var meditationList = JsonConvert.DeserializeObject<MeditationModel[]>(json);
            //        return meditationList;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("FetchAllMeditation", ex.Message);
            //}

            //return null;
        }

        public Task<KnowledgeBaseModel> fetchKnowledgeBaseUsingIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
