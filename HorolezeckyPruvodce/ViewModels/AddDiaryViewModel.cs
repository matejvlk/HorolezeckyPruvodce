using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;
using Xamarin.Forms;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels
{
    public class AddDiaryViewModel : ViewModel
    {
        public List<string> Styles = new List<string> { "On Sight", "Flash", "Red Point", "Pink Point", "Red Cross", "All Free", "Top Rope", "Free Solo", "Deep Water Solo" };

        private Route model;

        private Diary editingDiary;
        //private string sectorName;

        public AddDiaryViewModel(Route route, Diary diary)
        {
            this.model = route;
            this.editingDiary = diary;
            if (diary != null)
            {
                this.SelectedStyleIndex = Styles.IndexOf(diary.Style);
                this.FeelingValue = diary.Feeling;
                this.Date = diary.Date;
                this.PartnerTextInput = diary.Partner;
                this.NoteTextInput = diary.Note;
            }
            else
            {
                this.selectedStyleIndex = -1;
                this.FeelingValue = 5;
                this.Date = DateTime.Now;
            }

            this.SaveCommand = new Command(SaveCommand_Execute);

            this.GetSectorName();
        }

        private async void GetSectorName()
        {
            this.SectorName = await Route.GetSectorName(model.SectorId);
        }

        private async void SaveCommand_Execute(object obj)
        {
            Diary diaryToSave;

            if (editingDiary != null)
            {
                diaryToSave = new Diary()
                {
                    DiaryId = editingDiary.DiaryId,
                    UserId = editingDiary.UserId,
                    RouteId = editingDiary.RouteId,
                    Date = this.date,
                    Style = SelectedStyleIndex == -1 ? null : Styles[SelectedStyleIndex],
                    Partner = PartnerTextInput,
                    Feeling = (int)Math.Round(FeelingValue, MidpointRounding.AwayFromZero),
                    Note = NoteTextInput
                };
            }
            else
            {
                User user = await App.Database.GetUser();
                diaryToSave = new Diary()
                {
                    UserId = user.UserId,
                    RouteId = model.RouteId,
                    Date = this.date,
                    Style = SelectedStyleIndex == -1 ? null : Styles[SelectedStyleIndex],
                    Partner = PartnerTextInput,
                    Feeling = (int)Math.Round(FeelingValue, MidpointRounding.AwayFromZero),
                    Note = NoteTextInput
                };
            }

            int result = await App.Database.SaveDiary(diaryToSave);
            var toast = DependencyService.Get<IToast>();
            if (result == 1)
                toast.ShowToast(AppResources.AddDiaryPage_Saved);
            else
                toast.ShowToast(AppResources.AddDiaryPage_NotSaved);
        }

        public string RouteName => model != null && model.Name != null ? model.Name : String.Empty;
        public string Grade => Difficulty.GetGrade(model);
        public string GradingSystem => Difficulty.GetGradingSystem(model);

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

        private int selectedStyleIndex;
        public int SelectedStyleIndex
        {
            get { return this.selectedStyleIndex; }
            set
            {
                selectedStyleIndex = value;
                this.OnPropertyChanged();
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private double feelingValue;
        public double FeelingValue
        {
            get { return feelingValue; }
            set { feelingValue = value; }
        }

        private string partnerTextInput;
        public string PartnerTextInput
        {
            get { return partnerTextInput; }
            set { partnerTextInput = value; }
        }

        private string noteTextInput;
        public string NoteTextInput
        {
            get { return noteTextInput; }
            set { noteTextInput = value; }
        }

        public ICommand SaveCommand { get; private set; }
    }
}
