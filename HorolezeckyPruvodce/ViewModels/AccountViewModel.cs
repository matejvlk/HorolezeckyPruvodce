using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels;
using Xamarin.Forms;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels
{
    public class AccountViewModel : ViewModel
    {
        private List<DiaryItemViewModel> diaries;

        public AccountViewModel()
        {
            this.CreateAccountCommand = new Command(CreateAccountCommand_Execute);
        }

        public async Task LoadData()
        {
            User user = await App.Database.GetUser();
            if (user == null)
            {
                UserLoggedId = false;
            }
            else
            {
                UserLoggedId = true;
                UserName = user.Name;
                List<Diary> result = await App.Database.GetDiariesByUserId(user.UserId);
                NumberOfDiaryRoutes = result.Count;
                AverageDifficulty = await GetAverageDifficulty(result);
                Diaries = await ConvertData(result);
            }
        }

        private async Task<List<DiaryItemViewModel>> ConvertData(List<Diary> list)
        {
            var output = new List<DiaryItemViewModel>();
            if (list != null && list.Count > 0)
            {
                list.Sort((x, y) => y.Date.CompareTo(x.Date));

                DateTime currentDate = list[0].Date.Date;
                List<Diary> sameDayDiaries = new List<Diary>();
                string sectorNames;
                List<Route> sameDayRoutes = new List<Route>();

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Date.Date == currentDate)
                    {
                        sameDayDiaries.Add(list[i]);
                    }
                    else
                    {
                        sectorNames = await GetSectorNames(sameDayDiaries);
                        sameDayRoutes = await GetRoutes(sameDayDiaries);
                        output.Add(new DiaryItemViewModel(sameDayDiaries, sectorNames, sameDayRoutes));
                        sameDayDiaries = new List<Diary>();
                        currentDate = list[i].Date.Date;
                        sameDayDiaries.Add(list[i]);
                    }
                }
                //nakonec přidáme item s posledním datem, který nám zbyl
                sectorNames = await GetSectorNames(sameDayDiaries);
                sameDayRoutes = await GetRoutes(sameDayDiaries);
                output.Add(new DiaryItemViewModel(sameDayDiaries, sectorNames, sameDayRoutes));
            }
            return output;
        }

        private async Task<string> GetAverageDifficulty(List<Diary> diaries)
        {
            string output = "-";
            if (diaries != null && diaries.Count > 0)
            {
                int total = 0;
                int count = 0;
                foreach (var diary in diaries)
                {
                    Route r = await App.Database.GetRouteAsync(diary.RouteId);
                    if (!String.IsNullOrEmpty(r.Grade) && r.GradingSystem != null && r.GradingSystem != Difficulty.Other)
                    {
                        int grade;
                        int.TryParse(r.Grade, out grade);
                        if (grade != 0)
                        {
                            total += grade;
                            count++;
                        }
                    }
                }
                int averageVirtual = (int)Math.Round((double)total/count);
                output = Difficulty.GetSaxonFromVirtual(averageVirtual) + $" ({AppResources.GradingSystem_Saxon})";
            }
            return output;
        }

        private async Task<string> GetSectorNames(List<Diary> diaries)
        {
            string output = "";
            if (diaries != null && diaries.Count > 0)
            {
                List<int> sectorIDs = new List<int>();
                
                foreach (var diary in diaries)
                {
                    Route r = await App.Database.GetRouteAsync(diary.RouteId);
                    if (!sectorIDs.Contains(r.SectorId))
                    {
                        sectorIDs.Add(r.SectorId);
                    }
                }
                List<string> sectorNames = new List<string>();
                foreach (var sectorId in sectorIDs)
                {
                    Sector s = await App.Database.GetSector(sectorId);
                    sectorNames.Add(s.Name);
                }
                output = string.Join(", ", sectorNames);
            }
            return output;
        }

        private async Task<List<Route>> GetRoutes(List<Diary> diaries)
        {
            List<Route> output = new List<Route>();
            foreach (var diary in diaries)
            {
                Route r = await App.Database.GetRouteAsync(diary.RouteId);
                output.Add(r);
            }
            return output;
        }

        private async void CreateAccountCommand_Execute(object obj)
        {
            User user = new User
            {
                Name = UserNameTextInput
            };
            int result = await App.Database.SaveUser(user);
            if (result > -1)
            {
                UserName = user.Name;
                var toast = DependencyService.Get<IToast>();
                toast.ShowToast(AppResources.AccountPage_UserCreated);
                UserLoggedId = true;
            }
        }

        #region Properties

        public List<DiaryItemViewModel> Diaries
        {
            get { return diaries; }
            set
            {
                if (diaries != value)
                {
                    diaries = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        private int numberOfDiaryRoutes;
        public int NumberOfDiaryRoutes
        {
            get { return numberOfDiaryRoutes; }
            set
            {
                numberOfDiaryRoutes = value;
                OnPropertyChanged();
            }
        }

        private string averageDifficulty;
        public string AverageDifficulty
        {
            get { return averageDifficulty; }
            set
            {
                averageDifficulty = value;
                OnPropertyChanged();
            }
        }

        private string userNameTextInput;
        public string UserNameTextInput
        {
            get { return userNameTextInput; }
            set
            {
                userNameTextInput = value;
                OnPropertyChanged();
            }
        }

        private bool userLoggedIn;
        public bool UserLoggedId
        {
            get { return this.userLoggedIn; }
            set
            {
                if (value != userLoggedIn)
                {
                    this.userLoggedIn = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(UserNotLoggedId));
                }
            }
        }

        public bool UserNotLoggedId => !UserLoggedId;

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

        #endregion

        public ICommand CreateAccountCommand { get; private set; }
    }
}
