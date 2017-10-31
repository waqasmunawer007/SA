using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class DailyDigest
    {
        [PrimaryKey, Unique, NotNull]
        public string id { get; set; }
        public string text { get; set; }
        public string publish_date { get; set; }
    }
}
