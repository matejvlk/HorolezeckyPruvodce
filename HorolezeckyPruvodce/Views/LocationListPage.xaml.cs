using System;
using System.Collections.Generic;
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
    public partial class LocationListPage : ContentPage
    {
        public LocationListPage()
        {
            InitializeComponent();
            //this.BackgroundColor = (Color)App.Current.Resources["PrimaryDarkColor"];
        }

        protected override async void OnAppearing()
        {
            await (this.BindingContext as LocationListViewModel).LoadData();
        }

        private async void Location_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AreaListPage((e.SelectedItem as LocationItemViewModel).Location));
            }
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView)
            {
                (sender as ListView).SelectedItem = null;
            }
        }
    }
}
