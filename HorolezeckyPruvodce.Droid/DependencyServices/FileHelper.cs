using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UHK.MatejVlk.HorolezeckyPruvodce.Droid.DependencyServices;
using Xamarin.Forms;
using UHK.MatejVlk.HorolezeckyPruvodce.Backend.DataAccess.Database.Interfaces;

[assembly: Dependency(typeof(FileHelper))]
namespace UHK.MatejVlk.HorolezeckyPruvodce.Droid.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}