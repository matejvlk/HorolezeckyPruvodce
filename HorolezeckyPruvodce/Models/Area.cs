using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Models
{
    public class Area
    {
        public static async Task<string> GetLocationName(int locationId)
        {
            string locationName = "";
            var location = await App.Database.GetLocationAsync(locationId);
            locationName = location.Name;
            return locationName;
        }

        [PrimaryKey, AutoIncrement, NotNull]
        public int AreaId { get; set; }

        [NotNull]
        public int LocationId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageNames { get; set; }
    }
}
