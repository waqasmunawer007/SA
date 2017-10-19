using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class Step
    {
        [PrimaryKey,Unique,AutoIncrement,NotNull]
        public int id { get; set; }
        public string step { get; set; }
        public string meditation_id { get; set; }
    }
}
