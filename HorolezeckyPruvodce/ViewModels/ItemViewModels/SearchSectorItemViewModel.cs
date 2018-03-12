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
    public class SearchSectorItemViewModel : ViewModel
    {
        private Sector sector;

        public SearchSectorItemViewModel(Sector sector)
        {
            this.sector = sector;
            this.GetAreaName();
        }

        private async void GetAreaName()
        {
            this.AreaName = await Sector.GetAreaName(sector.AreaId);
        }

        public string SectorName => sector.Name;

        private string areaName;
        public string AreaName
        {
            get { return this.areaName; }
            set
            {
                this.areaName = value;
                this.OnPropertyChanged();
            }
        }

        public Sector Sector => sector;
    }
}
