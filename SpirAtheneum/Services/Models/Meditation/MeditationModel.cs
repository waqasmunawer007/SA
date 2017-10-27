using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Meditation
{
    public class MeditationModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string intro { get; set; }
        public List<string> steps { get; set; }
        public string html_string { get; set; }
        public string outro { get; set; }
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
