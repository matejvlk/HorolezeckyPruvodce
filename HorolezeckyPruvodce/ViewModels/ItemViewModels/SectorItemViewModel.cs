using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class SectorItemViewModel : ViewModel
    {
        private Sector model;

        public SectorItemViewModel(Sector sector)
        {
            this.model = sector;
        }

        public string Name => model.Name;

        public Sector Sector => this.model;
    }
}
