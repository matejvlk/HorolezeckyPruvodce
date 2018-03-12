using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class AreaItemViewModel : ViewModel
    {
        private Area model;

        public AreaItemViewModel(Area area)
        {
            this.model = area;
        }

        public string Name => model.Name;

        public Area Area => this.model;
    }
}
