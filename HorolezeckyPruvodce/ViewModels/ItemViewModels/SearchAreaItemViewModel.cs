using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class SearchAreaItemViewModel : ViewModel
    {
        private Area area;

        public SearchAreaItemViewModel(Area area)
        {
            this.area = area;
            this.GetLocationName();
        }

        private async void GetLocationName()
        {
            this.LocationName = await Area.GetLocationName(area.LocationId);
        }

        public string AreaName => area.Name;

        private string locationName;
        public string LocationName
        {
            get { return this.locationName; }
            set
            {
                this.locationName = value;
                this.OnPropertyChanged();
            }
        }

        public Area Area => area;
    }
}
