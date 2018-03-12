using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels;
using Xamarin.Forms;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels
{
    public class LocationListViewModel : ViewModel
    {
        private List<LocationItemViewModel> locations;

        public async Task LoadData()
        {
            List<Location> result = await App.Database.GetAllLocationsAsync();
            this.Locations = ConvertData(result);
        }

        private List<LocationItemViewModel> ConvertData(List<Location> list)
        {
            var output = new List<LocationItemViewModel>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    output.Add(new LocationItemViewModel(item));
                }
            }
            return output;
        }

        public List<LocationItemViewModel> Locations
        {
            get { return locations; }
            set
            {
                if (locations != value)
                {
                    locations = value;
                    this.OnPropertyChanged(nameof(Locations));
                }
            }
        }
    }
}
