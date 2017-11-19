using System;
using System.Collections.Generic;
using Services.Models.KnowledgeBase;
using Services.Models.Meditation;
using Services.Models.Subscription;

namespace Services.Models.Favourite
{
    public class FevouriteRequest
    {
        public string id { get; set; }
        public string mobile_user_id { get; set; }
        public List<MeditationModel> meditations { get; set; }
        public List<KnowledgeBaseModel> knowledge { get; set; }
    }


}
