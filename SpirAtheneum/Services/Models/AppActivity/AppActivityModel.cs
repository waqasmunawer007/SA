using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.AppActivity
{
    public class AppActivityModel : BaseResponse
    {
        public string id { get; set; }
        public DigestActivity digest_activity { get; set; }
        public MeditationsActivity meditation_activity { get; set; }
        public KnowledgeActivity knowledge_activity { get; set; }
    }
    public class DigestActivity
    {
        public string last_updated { get; set; }
    }

    public class MeditationsActivity
    {
        public string last_updated { get; set; }
    }

    public class KnowledgeActivity
    {
        public string last_updated { get; set; }
    }
}