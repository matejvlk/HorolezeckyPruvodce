using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels
{
    public class DiaryRouteItemViewModel : ViewModel
    {
        private Diary diary;
        private Route route;

        public DiaryRouteItemViewModel(Diary diary, Route route)
        {
            this.diary = diary;
            this.route = route;
        }

        public string Date => diary.Date.Date.ToString("d. M. yyyy");

        public string DiaryText
        {
            get { return this.GetDiaryText(); }
        }

        public Route Route => this.route;
        public Diary Diary => this.diary;

        private string GetDiaryText()
        {
            List<string> listOfInformations = new List<string>();
            string routeName = "";
            if (!string.IsNullOrEmpty(route.Grade))
                routeName = $"{Difficulty.GetGrade(route)} - ";
            routeName += route.Name;
            listOfInformations.Add(routeName);
            
            if(!string.IsNullOrEmpty(diary.Style))
                listOfInformations.Add(diary.Style);

            listOfInformations.Add($"{diary.Feeling}/10");

            if (!string.IsNullOrEmpty(diary.Partner))
                listOfInformations.Add(diary.Partner);

            if (!string.IsNullOrEmpty(diary.Note))
                listOfInformations.Add(diary.Note);

            return string.Join(", ", listOfInformations);
        }
    }
}
