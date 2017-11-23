using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class Meditation
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int dbID { get; set; }

        public string id { get; set; }
        public string title { get; set; }
        public string html_string { get; set; }
        public string email { get; set; }
        public string share_message { get; set; }
        public string category { get; set; }
    }
}
