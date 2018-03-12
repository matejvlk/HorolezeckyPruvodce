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
    public class RouteItemViewModel : ViewModel
    {
        private Route model;

        public RouteItemViewModel(Route route)
        {
            this.model = route;
        }

        public string Name => model.ViewOrder != null ? $"{model.ViewOrder}. {model.Name} - {Difficulty.GetGrade(model)}" : $"{model.Name} - {Difficulty.GetGrade(model)}";

        public Route Route => this.model;
    }
}
