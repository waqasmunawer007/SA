using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class FavouriteKnowledgeBase
    {
        [PrimaryKey, Unique, NotNull]
        public string id { get; set; }
        public string is_favourite { get; set; }
    }
}
