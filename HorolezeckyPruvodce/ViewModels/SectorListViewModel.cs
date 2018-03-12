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
    public class SectorListViewModel : ViewModel
    {
        private List<SectorItemViewModel> sectors;
        private Area area;

        public SectorListViewModel(Area area)
        {
            this.area = area;
            this.GetLocationName();
        }

        private async void GetLocationName()
        {
            this.LocationName = await Area.GetLocationName(area.LocationId);
        }

        public async Task LoadData()
        {
            List<Sector> result = await App.Database.GetSectorsByAreaId(area.AreaId);
            this.Sectors = ConvertData(result);
        }

        private List<SectorItemViewModel> ConvertData(List<Sector> list)
        {
            var output = new List<SectorItemViewModel>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    output.Add(new SectorItemViewModel(item));
                }
            }
            return output;
        }

        public List<SectorItemViewModel> Sectors
        {
            get { return sectors; }
            set
            {
                if (sectors != value)
                {
                    sectors = value;
                    this.OnPropertyChanged(nameof(Sectors));
                }
            }
        }

        public string AreaName => $"{locationName} > {area.Name}";

        private string locationName;
        public string LocationName
        {
            get { return this.locationName; }
            set
            {
                this.locationName = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(AreaName));
            }
        }

        public string AreaDescription => String.IsNullOrEmpty(area.Description) ? AppResources.SectorListPage_NoAreaInfo : area.Description;
        
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
