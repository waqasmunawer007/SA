﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirAtheneum.Models
{
    public class Meditation
    {
        [PrimaryKey, Unique, NotNull]
        public string id { get; set; }
        public string title { get; set; }
        public string intro { get; set; }
        public string outro { get; set; }
        public string category { get; set; }
    }
}
