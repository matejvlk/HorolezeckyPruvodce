using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels;
using Xamarin.Forms;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Guide_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationListPage());
        }

        private async void Search_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void Account_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        private async void AboutApp_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutAppPage());
        }
    }
}
