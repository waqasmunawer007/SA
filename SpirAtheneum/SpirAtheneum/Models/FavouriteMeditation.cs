using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class FavouriteMeditation
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int dbID { get; set; }

        public string id { get; set; }
        public string email { get; set; }
        public string is_favourite { get; set; }
    }
}
