using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels
{
    public class RouteDetailViewModel : ViewModel
    {
        private Route model;
        
        public RouteDetailViewModel(Route route)
        {
            this.model = route;
            this.GetSectorName();
        }

        private async void GetSectorName()
        {
            this.SectorName = await Route.GetSectorName(model.SectorId);
        }

        public string Title => model != null && model.Name != null ? model.Name : String.Empty;
        public string Grade => Difficulty.GetGrade(model);
        public string GradingSystem => Difficulty.GetGradingSystem(model);
        public string Author => model != null && !string.IsNullOrEmpty(model.Author) ? model.Author : AppResources.RouteDetailPage_UnknownAuthor;
        public string Type => model != null && model.Type != null ? model.Type : String.Empty;
        public string RockType => model != null && model.RockType != null ? model.RockType : String.Empty;
        public string Difficulties => model != null && model.Difficulties != null ? model.Difficulties : String.Empty;
        public string Angle => model != null && model.Angle != null ? model.Angle : String.Empty;
        public string Length => model != null && model.Length != 0 ? $"{model.Length} m" : String.Empty;
        public string Protection => model != null && model.Protection != null ? model.Protection : String.Empty;
        public string Description => model != null && model.Description != null ? model.Description : String.Empty;
        public string Note => model != null && model.Note != null ? model.Note : String.Empty;

        public Route Model => this.model;

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
    }
}
