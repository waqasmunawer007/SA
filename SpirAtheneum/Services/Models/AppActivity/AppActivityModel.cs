using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.AppActivity
{
    public class AppActivityModel
    {
        public string id { get; set; }
        public DailydigestLastupdated dailydigest_lastupdated { get; set; }
        public MeditationsLastupdated meditations_lastupdated { get; set; }
        public KnowledgeLastupdated knowledge_lastupdated { get; set; }
    }
    public class DailydigestLastupdated
    {
        public string last_updated { get; set; }
    }

    public class MeditationsLastupdated
    {
        public string last_updated { get; set; }
    }

    public class KnowledgeLastupdated
    {
        public string last_updated { get; set; }
    }
}
