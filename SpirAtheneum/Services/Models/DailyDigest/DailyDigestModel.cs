using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.DailyDigest
{
   public class DailyDigestModel : BaseResponse
    {
        public string id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string category { get; set; }
        public DateTime publish_date { get; set; }
        public Meditation.Meta meta { get; set; }
    }
}
