using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Droid
{
    [Activity(Label = "Horolezecký Průvodce", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //následující musí být voláno až po base.OnCreate()
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.SetStatusBarColor(new Android.Graphics.Color(Android.Support.V4.Content.ContextCompat.GetColor(this, Resource.Color.PrimaryDarkColor)));
            }

            //nakopírování předvyplněné databáze
            CopyExistingDatabase();

            //nakopírování obrázků
            CopyImages();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }


        private void CopyExistingDatabase()
        {
            string DB_ANAGRAFICA_FILENAME = "HorolezeckyPruvodce.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbFile = Path.Combine(folderPath, DB_ANAGRAFICA_FILENAME); // FILE NAME TO USE WHEN COPIED

            /*
            //vždy nakopírujeme novou databázi - smažeme pokud již nějaká existuje z minula
            if (System.IO.File.Exists(dbFile))
            {
                System.IO.File.Delete(dbFile);
            }

            //překopírování databáze se složky Assets do "pracovní" složky androidu
            using (BinaryReader br = new BinaryReader(Assets.Open(DB_ANAGRAFICA_FILENAME)))
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(dbFile, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int len = 0;
                    while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, len);
                    }
                }
            }
            */

            //kopírujeme pouze pokud neexistuje (aby nám zůstal vytvořený profil)
            if (!System.IO.File.Exists(dbFile))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(DB_ANAGRAFICA_FILENAME)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbFile, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }

        private void CopyImages()
        {
            string imagesFolderName = "images";
            string androidPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completeAndroidPath = Path.Combine(androidPath, imagesFolderName); // FILE NAME TO USE WHEN COPIED
            Directory.CreateDirectory(completeAndroidPath);
            
            if (!System.IO.File.Exists(completeAndroidPath))
            {
                //překopírování obrázků ze složky Assets do "pracovní" složky androidu
                string[] imageNames = Assets.List(imagesFolderName);

                foreach (var image in imageNames)
                {
                    string assetsPath = Path.Combine(imagesFolderName, image);
                    string writePath = Path.Combine(completeAndroidPath, image);

                    using (BinaryReader br = new BinaryReader(Assets.Open(assetsPath)))
                    {
                        using (BinaryWriter bw = new BinaryWriter(new FileStream(writePath, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int len = 0;
                            while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, len);
                            }
                        }
                    }
                }
            }
        }
    }
}

