using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UHK.MatejVlk.HorolezeckyPruvodce.Backend.DataAccess.Database;
using UHK.MatejVlk.HorolezeckyPruvodce.Backend.DataAccess.Database.Interfaces;
using UHK.MatejVlk.HorolezeckyPruvodce.Database;
using UHK.MatejVlk.HorolezeckyPruvodce.Interfaces;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.Views;
using Xamarin.Forms;

namespace UHK.MatejVlk.HorolezeckyPruvodce
{
    public partial class App : Application
    {
        private static HorolezeckyPruvodceDatabase database;
        private static string imagesFolder;

        public App()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new NavigationPage(new HomePage());
        }

        public static HorolezeckyPruvodceDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new HorolezeckyPruvodceDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("HorolezeckyPruvodce.sqlite"));
                }
                return database;
            }
        }

        public static string ImagesFolder
        {
            get
            {
                if (imagesFolder == null)
                {
                    imagesFolder = DependencyService.Get<IFileHelper>().GetLocalFilePath("images");
                }
                return imagesFolder;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
