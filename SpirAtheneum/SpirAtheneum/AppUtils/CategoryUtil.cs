using Services.Models.DailyDigest;
using Services.Models.Meditation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpirAtheneum.ViewModels.MeditationViewModel.MeditationVM;

namespace SpirAtheneum.AppUtils
{
    class CategoryUtil
    {
        
        public static List<Category> GetCount(MeditationModel[] med)   // find and separate the Meditation category
        {
            List<Category> list   = new List<Category>();
            var r = med.GroupBy(e => e.category).Select(g => new { count = g.Count(), category = g.Key, title = g.First().category });
           foreach (var m in r)
            {
                Category c = new Category();
                c.title =  m.count +" " + "Meditation";  // cancate count and first item title to show in {count}/{title}s format in main UI list cell
                c.count =m.count;
                c.category = m.category;
                list.Add(c);
            }
            return list;

        }
        public static List<Category> GetDigestCategoriesDetail(DailyDigestModel[] allDigest)   // find and separate the Digest category and give the total number of items agaist every category
        {
            List<Category> list = new List<Category>();
            var r = allDigest.GroupBy(e => e.category).Select(g => new { count = g.Count(), category = g.Key, title = g.First().title });
            foreach (var m in r)
            {
                Category c = new Category(); //this class define in MeditationVM
                c.title = "{" + m.count + "}" + " " + "{" + m.title + "}";  // cancate count and first item title to show in {count}/{title}s format in main UI list cell
                c.count = m.count;
                c.category = m.category;
                list.Add(c);
            }
            return list;

        }


    }
}
