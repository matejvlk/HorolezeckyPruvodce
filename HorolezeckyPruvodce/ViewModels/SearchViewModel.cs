using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels;
using Xamarin.Forms;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels
{
    public class SearchViewModel : ViewModel
    {
        private List<SearchRouteItemViewModel> foundRoutes;
        private List<SearchSectorItemViewModel> foundSectors;
        private List<SearchAreaItemViewModel> foundAreas;
        private List<SearchLocationItemViewModel> foundLocations;

        public SearchViewModel()
        {
            this.SearchCommand = new Command(SearchCommand_Execute);
        }

        private async void SearchCommand_Execute(object obj)
        {
            if (SearchTextInput.Length >= 2)
            {
                var routes = await App.Database.SearchRoutes(SearchTextInput);
                FoundRoutes = this.ConvertRoutesData(routes);

                var sectors = await App.Database.SearchSectors(SearchTextInput);
                FoundSectors = this.ConvertSectorsData(sectors);

                var areas = await App.Database.SearchAreas(SearchTextInput);
                FoundAreas = this.ConvertAreasData(areas);

                var locations = await App.Database.SearchLocations(SearchTextInput);
                FoundLocations = this.ConvertLocationsData(locations);
            }
            else
            {
                var toast = DependencyService.Get<IToast>();
                toast.ShowToast(AppResources.SearchPage_MinLength);
            }
        }

        private List<SearchRouteItemViewModel> ConvertRoutesData(List<Route> list)
        {
            var output = new List<SearchRouteItemViewModel>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    output.Add(new SearchRouteItemViewModel(item));
                }
            }
            return output;
        }

        private List<SearchSectorItemViewModel> ConvertSectorsData(List<Sector> list)
        {
            var output = new List<SearchSectorItemViewModel>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    output.Add(new SearchSectorItemViewModel(item));
                }
            }
            return output;
        }

        private List<SearchAreaItemViewModel> ConvertAreasData(List<Area> list)
        {
            var output = new List<SearchAreaItemViewModel>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    output.Add(new SearchAreaItemViewModel(item));
                }
            }
            return output;
        }

        private List<SearchLocationItemViewModel> ConvertLocationsData(List<Location> list)
        {
            var output = new List<SearchLocationItemViewModel>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    output.Add(new SearchLocationItemViewModel(item));
                }
            }
            return output;
        }

        #region Properties

        public List<SearchRouteItemViewModel> FoundRoutes
        {
            get { return foundRoutes; }
            set
            {
                if (foundRoutes != value)
                {
                    foundRoutes = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public List<SearchSectorItemViewModel> FoundSectors
        {
            get { return foundSectors; }
            set
            {
                if (foundSectors != value)
                {
                    foundSectors = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public List<SearchAreaItemViewModel> FoundAreas
        {
            get { return foundAreas; }
            set
            {
                if (foundAreas != value)
                {
                    foundAreas = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public List<SearchLocationItemViewModel> FoundLocations
        {
            get { return foundLocations; }
            set
            {
                if (foundLocations != value)
                {
                    foundLocations = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private string searchTextInput;
        public string SearchTextInput
        {
            get { return searchTextInput; }
            set
            {
                searchTextInput = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ICommand SearchCommand { get; private set; }
    }
}
