using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Models
{
    public class Route
    {
        public static async Task<string> GetSectorName(int sectorId)
        {
            string sectorName = "";
            var sector = await App.Database.GetSector(sectorId);
            sectorName = sector.Name;
            var area = await App.Database.GetArea(sector.AreaId);
            sectorName = area.Name + " > " + sectorName;
            var location = await App.Database.GetLocationAsync(area.LocationId);
            sectorName = location.Name + " > " + sectorName;
            return sectorName;
        }

        [PrimaryKey, AutoIncrement, NotNull]
        public int RouteId { get; set; }

        [NotNull]
        public int SectorId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Grade { get; set; }

        public string GradingSystem { get; set; }

        public string Author { get; set; }

        public string Type { get; set; }

        public string RockType { get; set; }

        public string Difficulties { get; set; }

        public string Angle { get; set; }

        public int Length { get; set; }

        public string Protection { get; set; }
        
        public string Description { get; set; }

        public string Note { get; set; }

        public string ImageNames { get; set; }

        public string ViewOrder { get; set; }
    }
}
