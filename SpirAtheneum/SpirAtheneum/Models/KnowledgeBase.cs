using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class KnowledgeBase
    {
        [PrimaryKey, Unique, NotNull]
        public string id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string share_message { get; set; }
        public string category { get; set; }
    }
}
