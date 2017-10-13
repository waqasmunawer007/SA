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
        
        public static List<Category> GetCount(MeditationModel[] med)
        {
            List<Category> list   = new List<Category>();
            var r = med.GroupBy(e => e.category).Select(g => new { count = g.Count(), category = g.Key, title = g.First().title });
           foreach (var m in r)
            {
                Category c = new Category();
                c.title = "{" + m.count + "}"+" " + "{" + m.title + "}";
                c.count =m.count;
                c.category = m.category;
                list.Add(c);
            }
            return list;

        }
    }
}
