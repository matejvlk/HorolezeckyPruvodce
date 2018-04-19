using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Foundation;
using UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices;
using UHK.MatejVlk.HorolezeckyPruvodce.iOS.DependencyServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Dialogs))]
namespace UHK.MatejVlk.HorolezeckyPruvodce.iOS.DependencyServices
{
    public class Dialogs : IDialogs
    {
        /// <summary>
        /// zobrazí dialog s nadpisem, zprávou a dvěmi tlačítky
        /// </summary>
        /// <param name="title">nadpis</param>
        /// <param name="message">zpráva</param>
        /// <param name="positiveButtonText">text pro levé tlačítko</param>
        /// <param name="negativeButtonText">text pro pravé tlačítko</param>
        /// <param name="positiveButton">akce pro levé tlačítko</param>
        /// <param name="negativeButton">ance pro pravé tlačítko</param>
        public void ShowDialog(string title, string message, string positiveButtonText, string negativeButtonText, EventHandler positiveButton, EventHandler negativeButton)
        {
            if (Controller.PresentedViewController == null)
            {
                var dialog = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                if (!string.IsNullOrEmpty(positiveButtonText))
                    dialog.AddAction(UIAlertAction.Create(positiveButtonText, UIAlertActionStyle.Default, (e) => { positiveButton?.Invoke(null, null); }));
                if (!string.IsNullOrEmpty(negativeButtonText))
                    dialog.AddAction(UIAlertAction.Create(negativeButtonText, UIAlertActionStyle.Default, (e) => { negativeButton?.Invoke(null, null); }));
                Controller.PresentViewController(dialog, true, null);
            }
        }

        private UIViewController Controller
        {
            get
            {
                var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
                if (controller.PresentedViewController != null)
                {
                    return controller.PresentedViewController;
                }
                else
                {
                    return controller;
                }
            }
        }
    }
}