using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class KnowledgeBaseBinding
    {
        public string id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string category { get; set; }
        public string share_message { get; set; }
        public string is_favourite { get; set; }
    }
}
