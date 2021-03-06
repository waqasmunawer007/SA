﻿using Services.Models.DailyDigest;
using Services.Models.KnowledgeBase;
using Services.Models.Meditation;
using SpirAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.AppUtils
{
    class CategoryUtil
    {
        
        public static List<Category> GetCountMeditation(List<Meditation> meditation)   // find and separate the Meditation category
        {
            List<Category> list   = new List<Category>();
            var result = meditation.GroupBy(e => e.category).Select(g => new { count = g.Count(), category = g.Key, title = g.First().category });
           foreach (var m in result)
            {
                Category c = new Category();
                c.title =  m.count +" " + "Meditation";  // cancate count and first item title to show in {count}/{title}s format in main UI list cell
                c.count =m.count;
                c.category = m.category;
                list.Add(c);
            }
            return list;

        }

        public static List<Category> GetCountKnowledgeBase(List<KnowledgeBase> knowledgeBase)   // find and separate the KnowledgeBase category
        {
            List<Category> list = new List<Category>();
            var result = knowledgeBase.GroupBy(e => e.category).Select(g => new { count = g.Count(), category = g.Key, title = g.First().category });
            foreach (var k in result)
            {
                Category c = new Category();
                c.title = k.count + " " + "KnowledgeBase";  // cancate count and first item title to show in {count}/{title}s format in main UI list cell
                c.count = k.count;
                c.category = k.category;
                list.Add(c);
            }
            return list;

        }
    }
}
