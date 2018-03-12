using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels
{
    public class AreaListViewModel : ViewModel
    {
        private List<AreaItemViewModel> areas;
        private Location location;

        public AreaListViewModel(Location location)
        {
            this.location = location;
        }

        public async Task LoadData()
        {
            List<Area> result = await App.Database.GetAreasByLocationId(location.LocationId);
            this.Areas = ConvertData(result);
        }

        private List<AreaItemViewModel> ConvertData(List<Area> list)
        {
            var output = new List<AreaItemViewModel>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    output.Add(new AreaItemViewModel(item));
                }
            }
            return output;
        }

        public List<AreaItemViewModel> Areas
        {
            get { return areas; }
            set
            {
                if (areas != value)
                {
                    areas = value;
                    this.OnPropertyChanged(nameof(Areas));
                }
            }
        }

        public string LocationName => location.Name;

        public string LocationDescription => String.IsNullOrEmpty(location.Description) ? AppResources.AreaListPage_NoLocationInfo : location.Description;
        
        private bool expanded;
        public bool Expanded
        {
            get { return this.expanded; }
            set
            {
                if (this.expanded != value)
                {
                    this.expanded = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
