using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(UHK.MatejVlk.HorolezeckyPruvodce.Droid.DependencyServices.Dialogs))]
namespace UHK.MatejVlk.HorolezeckyPruvodce.Droid.DependencyServices
{
    public class Dialogs : IDialogs
    {
        private static AlertDialog dialog;
        
        /// <summary>
        /// zobrazí dialog s nadpisem, zprávou a dvěmi tlačítky
        /// </summary>
        /// <param name="title">nadpis</param>
        /// <param name="message">zpráva</param>
        /// <param name="positiveButtonText">text pro levé tlačítko</param>
        /// <param name="negativeButtonText">text pro pravé tlačítko</param>
        /// <param name="positiveButton">akce pro levé tlačítko</param>
        /// <param name="negativeButton">ance pro pravé tlačítko</param>
        /// <returns>dialog</returns>
        public void ShowDialog(string title, string message, string positiveButtonText, string negativeButtonText, EventHandler positiveButton, EventHandler negativeButton)
        {
            if (dialog == null || !dialog.IsShowing)
            {
                var positiveHandler = new EventHandler<DialogClickEventArgs>((sender, e) =>
                {
                    positiveButton?.Invoke(sender, EventArgs.Empty);
                });

                var negativeHandler = new EventHandler<DialogClickEventArgs>((sender, e) =>
                {
                    negativeButton?.Invoke(sender, EventArgs.Empty);
                });
                
                dialog = new AlertDialog.Builder(Forms.Context)
                    .SetTitle(title)
                    .SetMessage(message)
                    .SetPositiveButton(positiveButtonText, positiveHandler)
                    .SetNegativeButton(negativeButtonText, negativeHandler)
                    .Create();
                dialog.Show();
            }
        }
    }
}