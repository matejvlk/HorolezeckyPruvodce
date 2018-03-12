﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Models
{
    public class Location
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int LocationId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageNames { get; set; }
    }
}
