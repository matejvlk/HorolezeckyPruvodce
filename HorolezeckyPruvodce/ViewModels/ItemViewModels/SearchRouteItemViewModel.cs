using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class SearchRouteItemViewModel : ViewModel
    {
        private Route route;

        public SearchRouteItemViewModel(Route route)
        {
            this.route = route;
            this.GetSectorName();
        }

        private async void GetSectorName()
        {
            this.SectorName = await Route.GetSectorName(route.SectorId);
        }

        public string RouteName => $"{Difficulty.GetGrade(route)} - {route.Name}";
        
        private string sectorName;
        public string SectorName
        {
            get { return this.sectorName; }
            set
            {
                this.sectorName = value;
                this.OnPropertyChanged();
            }
        }

        public Route Route => route;
    }
}
