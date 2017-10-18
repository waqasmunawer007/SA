using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class Category
    {
        public int count { get; set; }   // number of items discover againt one category 
        public string title { get; set; }   // pick the first item's title  from the list of  all items shows againt one category
        public string category { get; set; }  // name of category
    }
}
