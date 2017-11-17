using System;
using System.Collections.Generic;
using Services.Models.Subscription;

namespace Services.Models.Favourite
{
    public class FevouriteRequest
    {
        public string id { get; set; }
        public string mobile_user_id { get; set; }
        public List<Meditation> meditations { get; set; }
        public List<Knowledge> knowledge { get; set; }
    }

    public class Knowledge
    {
        public string id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string pdf_link { get; set; }
        public string category { get; set; }
        public string share_message { get; set; }
        public Meta meta { get; set; }
    }

    public class Meditation
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
}
