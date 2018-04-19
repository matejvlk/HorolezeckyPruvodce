using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Foundation;
using UHK.MatejVlk.HorolezeckyPruvodce.Backend.DataAccess.Database.Interfaces;
using UHK.MatejVlk.HorolezeckyPruvodce.iOS.DependencyServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace UHK.MatejVlk.HorolezeckyPruvodce.iOS.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}