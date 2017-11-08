using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.KnowledgeBase
{
    public class KnowledgeBaseModel : BaseResponse
    {
        public string id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string pdf_link { get; set; }
        public string category { get; set; }
        public string share_message { get; set; }
        public Meta meta { get; set; }
    }
    public class Meta
    {
        public string author { get; set; }
        public DateTime date_added { get; set; }
        public DateTime last_edited { get; set; }
    }
}
