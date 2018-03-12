using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class DiaryItemViewModel : ViewModel
    {
        private List<DiaryRouteItemViewModel> listOfSameDateDiaries;
        private string sectorNames;

        public DiaryItemViewModel(List<Diary> diaries, string sectorNames, List<Route> routes)
        {
            this.listOfSameDateDiaries = this.ConvertData(diaries, routes);
            this.sectorNames = sectorNames;
        }

        private List<DiaryRouteItemViewModel> ConvertData(List<Diary> diaries, List<Route> routes)
        {
            List<DiaryRouteItemViewModel> output = new List<DiaryRouteItemViewModel>();
            for (int i = 0; i < diaries.Count; i++)
            {
                output.Add(new DiaryRouteItemViewModel(diaries[i], routes[i]));
            }
            return output;
        }

        public string Date => listOfSameDateDiaries[0].Date;

        public string NumberOfRoutes => $"{AppResources.AccountPage_NumberOfRoutes} {listOfSameDateDiaries.Count}";

        public string SectorNames => this.sectorNames;

        public List<DiaryRouteItemViewModel> ListOfSameDateDiaries => this.listOfSameDateDiaries;

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
