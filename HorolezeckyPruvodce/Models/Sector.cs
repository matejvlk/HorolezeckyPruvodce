using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Models
{
    public class Sector
    {
        public static async Task<string> GetAreaName(int areaId)
        {
            string areaName = "";
            var area = await App.Database.GetArea(areaId);
            areaName = area.Name;
            var location = await App.Database.GetLocationAsync(area.LocationId);
            areaName = location.Name + " > " + areaName;
            return areaName;
        }

        [PrimaryKey, AutoIncrement, NotNull]
        public int SectorId { get; set; }

        [NotNull]
        public int AreaId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageNames { get; set; }
    }
}
