using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class LocationItemViewModel : ViewModel
    {
        private Location model;

        public LocationItemViewModel(Location location)
        {
            this.model = location;
        }

        public string Name => model.Name;

        public Location Location => this.model;
    }
}
