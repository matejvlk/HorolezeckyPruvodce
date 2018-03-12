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
    public class RouteListViewModel : ViewModel
    {
        private List<RouteItemViewModel> routes;
        private Sector sector;

        public RouteListViewModel(Sector sector)
        {
            this.sector = sector;
            this.GetAreaName();
        }

        private async void GetAreaName()
        {
            this.AreaName = await Sector.GetAreaName(sector.AreaId);
        }

        public async Task LoadData()
        {
            List<Route> result = await App.Database.GetRoutesBySectorId(sector.SectorId);
            this.Routes = ConvertData(result);
        }

        private List<RouteItemViewModel> ConvertData(List<Route> list)
        {
            var output = new List<RouteItemViewModel>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    output.Add(new RouteItemViewModel(item));
                }
            }
            return output;
        }

        public List<RouteItemViewModel> Routes
        {
            get { return routes; }
            set
            {
                if (routes != value)
                {
                    routes = value;
                    this.OnPropertyChanged(nameof(Routes));
                }
            }
        }

        public string SectorName => $"{areaName} > {sector.Name}";

        private string areaName;
        public string AreaName
        {
            get { return this.areaName; }
            set
            {
                this.areaName = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(SectorName));
            }
        }

        public string SectorDescription => String.IsNullOrEmpty(sector.Description) ? AppResources.RouteListPage_NoSectorInfo : sector.Description;

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
