using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class SearchLocationItemViewModel : ViewModel
    {
        private Location location;

        public SearchLocationItemViewModel(Location location)
        {
            this.location = location;
        }
        
        public string LocationName => location.Name;

        public Location Location => location;
    }
}
