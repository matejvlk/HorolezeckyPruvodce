using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int UserId { get; set; }
        
        [NotNull]
        public string Name { get; set; }
    }
}
