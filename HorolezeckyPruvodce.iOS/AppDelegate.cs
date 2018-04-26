using System.IO;
using System.Threading;
using Foundation;
using PCLStorage;
using UIKit;

namespace UHK.MatejVlk.HorolezeckyPruvodce.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //nakopírování předvyplněné databáze
            CopyExistingDatabase();

            //nakopírování obrázků
            CopyImages();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }


        private void CopyExistingDatabase()
        {
            string DB_FILENAME = "HorolezeckyPruvodce.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbFile = Path.Combine(folderPath, DB_FILENAME); // FILE NAME TO USE WHEN COPIED

            /*
            //vždy nakopírujeme novou databázi - smažeme pokud již nějaká existuje z minula
            if (System.IO.File.Exists(dbFile))
            {
                System.IO.File.Delete(dbFile);
            }

            //překopírování databáze z root složky iOS projektu (součást instalačního balíčku) do "pracovní" složky iOSu
            System.IO.File.Copy(DB_FILENAME, dbFile);
            */

            //kopírujeme pouze pokud neexistuje (aby nám zůstal vytvořený profil)
            if (!System.IO.File.Exists(dbFile))
            {
                System.IO.File.Copy(DB_FILENAME, dbFile);
            }
        }

        private void CopyImages()
        {
            string imagesFolderName = "images";
            string iOSPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completeiOSPath = Path.Combine(iOSPath, imagesFolderName); // FILE NAME TO USE WHEN COPIED
            
            if (!System.IO.Directory.Exists(completeiOSPath))
            {
                Directory.CreateDirectory(completeiOSPath);
                //překopírování obrázků z root složky iOS projektu (součást instalačního balíčku) do "pracovní" složky iOSu
                string[] imageNames = Directory.GetFiles(imagesFolderName);

                foreach (var image in imageNames)
                {
                    string writePath = Path.Combine(iOSPath, image);
                    File.Copy(image, writePath);
                }
            }
        }
    }
}
