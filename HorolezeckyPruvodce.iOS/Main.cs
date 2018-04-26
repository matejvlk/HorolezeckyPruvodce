using System;
using UIKit;

namespace UHK.MatejVlk.HorolezeckyPruvodce.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex)
            {
                //pro odchycení exception v debug módu
                var x = ex;
            }
        }
    }
}
