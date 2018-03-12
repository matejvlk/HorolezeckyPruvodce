using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Models
{
    public class Diary
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int DiaryId { get; set; }

        [NotNull]
        public int UserId { get; set; }

        [NotNull]
        public int RouteId { get; set; }

        [NotNull]
        public DateTime Date { get; set; }

        public string Style { get; set; }

        public string Partner { get; set; }

        public int Feeling { get; set; } //0 - worst, 10 - best

        public string Note { get; set; }
    }
}
