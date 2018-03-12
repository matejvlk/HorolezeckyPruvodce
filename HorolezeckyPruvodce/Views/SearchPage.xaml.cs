using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            this.BindingContext = new SearchViewModel();
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as SearchViewModel).PropertyChanged += ViewModel_PropertyChanged;
        }

        protected override void OnDisappearing()
        {
            (this.BindingContext as SearchViewModel).PropertyChanged -= ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FoundRoutes" || e.PropertyName == "FoundSectors" || e.PropertyName == "FoundAreas" || e.PropertyName == "FoundLocations")
            {
                this.CreateFoundItemsView();
            }
        }

        private void CreateFoundItemsView()
        {
            this.FoundItemsStackLayout.Children.Clear();

            var vm = this.BindingContext as SearchViewModel;

            if (vm?.FoundRoutes != null && vm.FoundRoutes.Count > 0)
            {
                CreateFoundRoutesView(vm.FoundRoutes);
            }

            if (vm?.FoundSectors != null && vm.FoundSectors.Count > 0)
            {
                CreateFoundSectorsView(vm.FoundSectors);
            }

            if (vm?.FoundAreas != null && vm.FoundAreas.Count > 0)
            {
                CreateFoundAreasView(vm.FoundAreas);
            }

            if (vm?.FoundLocations != null && vm.FoundLocations.Count > 0)
            {
                CreateFoundLocationsView(vm.FoundLocations);
            }
        }

        private void CreateFoundRoutesView(List<SearchRouteItemViewModel> routes)
        {
            var lblFoundRoutesTitle = new Label
            {
                Text = AppResources.SearchPage_FoundRoutesTitle,
                TextColor = (Color)App.Current.Resources["PrimaryColor"],
                FontSize = (double)App.Current.Resources["ListFontSize"],
                Margin = new Thickness(10,10,10,0)
            };
            this.FoundItemsStackLayout.Children.Add(lblFoundRoutesTitle);

            var separatorTop = new BoxView
            {
                Margin = new Thickness(10,0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = (double)App.Current.Resources["SeparatorHeight"],
                BackgroundColor = (Color)App.Current.Resources["PrimaryColor"]
            };
            this.FoundItemsStackLayout.Children.Add(separatorTop);

            foreach (var route in routes)
            {
                var routeLayout = new StackLayout
                {
                    Margin = 10,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                routeLayout.BindingContext = route;

                var dotIcon = new Image
                {
                    Margin = new Thickness(10,0),
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "ic_dot_blue.png"
                };
                routeLayout.Children.Add(dotIcon);

                var rightLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                var lblSector = new Label
                {
                    FontSize = 15,
                    TextColor = (Color)App.Current.Resources["PrimaryDarkColor"]
                };
                lblSector.SetBinding(Label.TextProperty, new Binding("SectorName"));
                rightLayout.Children.Add(lblSector);

                var lblRouteName = new Label
                {
                    Text = route.RouteName,
                    TextColor = (Color)App.Current.Resources["PrimaryDarkColor"],
                    FontSize = 18
                };
                rightLayout.Children.Add(lblRouteName);
                routeLayout.Children.Add(rightLayout);

                var tgrRoute = new TapGestureRecognizer();
                tgrRoute.Tapped += async (sender, args) =>
                {
                    await Navigation.PushAsync(new RouteDetailPage(route.Route));
                };
                routeLayout.GestureRecognizers.Add(tgrRoute);
                
                this.FoundItemsStackLayout.Children.Add(routeLayout);
            }
        }

        private void CreateFoundSectorsView(List<SearchSectorItemViewModel> sectors)
        {
            var lblFoundSectorsTitle = new Label
            {
                Text = AppResources.SearchPage_FoundSectorsTitle,
                TextColor = (Color)App.Current.Resources["PrimaryColor"],
                FontSize = (double)App.Current.Resources["ListFontSize"],
                Margin = new Thickness(10,10,10,0)
            };
            this.FoundItemsStackLayout.Children.Add(lblFoundSectorsTitle);

            var separatorTop = new BoxView
            {
                Margin = new Thickness(10, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = (double)App.Current.Resources["SeparatorHeight"],
                BackgroundColor = (Color)App.Current.Resources["PrimaryColor"]
            };
            this.FoundItemsStackLayout.Children.Add(separatorTop);

            foreach (var sector in sectors)
            {
                var sectorLayout = new StackLayout
                {
                    Margin = 10,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                sectorLayout.BindingContext = sector;

                var dotIcon = new Image
                {
                    Margin = new Thickness(10,0),
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "ic_dot_blue.png"
                };
                sectorLayout.Children.Add(dotIcon);

                var rightLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                var lblArea = new Label
                {
                    FontSize = 15,
                    TextColor = (Color)App.Current.Resources["PrimaryDarkColor"]
                };
                lblArea.SetBinding(Label.TextProperty, new Binding("AreaName"));
                rightLayout.Children.Add(lblArea);

                var lblSectorName = new Label
                {
                    Text = sector.SectorName,
                    TextColor = (Color)App.Current.Resources["PrimaryDarkColor"],
                    FontSize = 18
                };
                rightLayout.Children.Add(lblSectorName);
                sectorLayout.Children.Add(rightLayout);

                var tgrSector = new TapGestureRecognizer();
                tgrSector.Tapped += async (sender, args) =>
                {
                    await Navigation.PushAsync(new RouteListPage(sector.Sector));
                };
                sectorLayout.GestureRecognizers.Add(tgrSector);

                this.FoundItemsStackLayout.Children.Add(sectorLayout);
            }
        }

        private void CreateFoundAreasView(List<SearchAreaItemViewModel> areas)
        {
            var lblFoundAreasTitle = new Label
            {
                Text = AppResources.SearchPage_FoundAreasTitle,
                TextColor = (Color) App.Current.Resources["PrimaryColor"],
                FontSize = (double) App.Current.Resources["ListFontSize"],
                Margin = new Thickness(10, 10, 10, 0)
            };
            this.FoundItemsStackLayout.Children.Add(lblFoundAreasTitle);

            var separatorTop = new BoxView
            {
                Margin = new Thickness(10, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = (double) App.Current.Resources["SeparatorHeight"],
                BackgroundColor = (Color) App.Current.Resources["PrimaryColor"]
            };
            this.FoundItemsStackLayout.Children.Add(separatorTop);

            foreach (var area in areas)
            {
                var areaLayout = new StackLayout
                {
                    Margin = 10,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                areaLayout.BindingContext = area;

                var dotIcon = new Image
                {
                    Margin = new Thickness(10, 0),
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "ic_dot_blue.png"
                };
                areaLayout.Children.Add(dotIcon);

                var rightLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                var lblLocation = new Label
                {
                    FontSize = 15,
                    TextColor = (Color) App.Current.Resources["PrimaryDarkColor"]
                };
                lblLocation.SetBinding(Label.TextProperty, new Binding("LocationName"));
                rightLayout.Children.Add(lblLocation);

                var lblAreaName = new Label
                {
                    Text = area.AreaName,
                    TextColor = (Color) App.Current.Resources["PrimaryDarkColor"],
                    FontSize = 18
                };
                rightLayout.Children.Add(lblAreaName);
                areaLayout.Children.Add(rightLayout);

                var tgrArea = new TapGestureRecognizer();
                tgrArea.Tapped += async (sender, args) =>
                {
                    await Navigation.PushAsync(new SectorListPage(area.Area));
                };
                areaLayout.GestureRecognizers.Add(tgrArea);

                this.FoundItemsStackLayout.Children.Add(areaLayout);
            }
        }

        private void CreateFoundLocationsView(List<SearchLocationItemViewModel> locations)
        {
            var lblFoundLocationsTitle = new Label
            {
                Text = AppResources.SearchPage_FoundLocationsTitle,
                TextColor = (Color)App.Current.Resources["PrimaryColor"],
                FontSize = (double)App.Current.Resources["ListFontSize"],
                Margin = new Thickness(10,10,10,0)
            };
            this.FoundItemsStackLayout.Children.Add(lblFoundLocationsTitle);

            var separatorTop = new BoxView
            {
                Margin = new Thickness(10, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = (double)App.Current.Resources["SeparatorHeight"],
                BackgroundColor = (Color)App.Current.Resources["PrimaryColor"]
            };
            this.FoundItemsStackLayout.Children.Add(separatorTop);

            foreach (var location in locations)
            {
                var locationLayout = new StackLayout
                {
                    Margin = 10,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                locationLayout.BindingContext = location;

                var dotIcon = new Image
                {
                    Margin = new Thickness(10,0),
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "ic_dot_blue.png"
                };
                locationLayout.Children.Add(dotIcon);
                
                var lblLocationName = new Label
                {
                    Text = location.LocationName,
                    TextColor = (Color)App.Current.Resources["PrimaryDarkColor"],
                    FontSize = 18
                };
                locationLayout.Children.Add(lblLocationName);

                var tgrLocation = new TapGestureRecognizer();
                tgrLocation.Tapped += async (sender, args) =>
                {
                    await Navigation.PushAsync(new AreaListPage(location.Location));
                };
                locationLayout.GestureRecognizers.Add(tgrLocation);

                this.FoundItemsStackLayout.Children.Add(locationLayout);
            }
        }
    }
}
