using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RouteDetailPage : ContentPage
    {
        public RouteDetailPage(Route route)
        {
            InitializeComponent();
            this.BindingContext = new RouteDetailViewModel(route);
            if (!String.IsNullOrEmpty(route.ImageNames))
            {
                string[] names = route.ImageNames.Split(';');
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
            this.CreateToolbarItems();
        }

        private async void CreateToolbarItems()
        {
            if (await App.Database.GetUser() != null)
            {
                var item = new ToolbarItem { Text = AppResources.AddDiaryPage_AddTitle, Icon = "ic_diary_add.png", Order = ToolbarItemOrder.Primary};
                item.Clicked += AddToDiary_Clicked;
                this.ToolbarItems.Add(item);
            }
        }

        private async void AddToDiary_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new AddDiaryPage((this.BindingContext as RouteDetailViewModel).Model));
        }
    }
}
