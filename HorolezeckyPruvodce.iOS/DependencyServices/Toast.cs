using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices;
using UIKit;

[assembly: Dependency(typeof(UHK.MatejVlk.HorolezeckyPruvodce.iOS.DependencyServices.Toast))]
namespace UHK.MatejVlk.HorolezeckyPruvodce.iOS.DependencyServices
{
    public class Toast : IToast
    {
        public async void ShowToast(string text)
        {
            var toast = new UIAlertView { Message = text };
            toast.Show();

            await Task.Delay(1500);
            toast.DismissWithClickedButtonIndex(0, true);
        }
    }
}