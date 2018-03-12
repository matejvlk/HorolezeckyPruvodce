using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteListPage : ContentPage
    {
        public RouteListPage(Sector sector)
        {
            InitializeComponent();
            this.BindingContext = new RouteListViewModel(sector);
            if (!String.IsNullOrEmpty(sector.ImageNames))
            {
                string[] names = sector.ImageNames.Split(';');
                foreach (var name in names)
                {
                    string path = Path.Combine(App.ImagesFolder, name);
                    Image image = new Image()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Aspect = Aspect.AspectFit
                    };
                    image.Source = ImageSource.FromFile(path);

                    this.ImagesLayout.Children.Add(image);
                }
            }
        }

        protected override async void OnAppearing()
        {
            await (this.BindingContext as RouteListViewModel).LoadData();
        }

        private async void Route_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new RouteDetailPage((e.SelectedItem as RouteItemViewModel).Route));
            }
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView)
            {
                (sender as ListView).SelectedItem = null;
            }
        }

        private async void ExpandIcon_Tapped(object sender, EventArgs e)
        {
            var vm = (this.BindingContext as RouteListViewModel);
            if (vm.Expanded)
            {
                this.ExpandIcon.RotateTo(0);
                await this.DetailLayout.FadeTo(0, 500);
                vm.Expanded = false;
            }
            else
            {
                vm.Expanded = true;
                this.ExpandIcon.RotateTo(180);
                this.DetailLayout.FadeTo(1, 500);
            }
        }
    }
}
