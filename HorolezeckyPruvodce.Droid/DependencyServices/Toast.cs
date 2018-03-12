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

[assembly: Dependency(typeof(UHK.MatejVlk.HorolezeckyPruvodce.Droid.DependencyServices.Toast))]
namespace UHK.MatejVlk.HorolezeckyPruvodce.Droid.DependencyServices
{
    public class Toast : IToast
    {
        public void ShowToast(string text)
        {
            Android.Widget.Toast.MakeText(Forms.Context, text, ToastLength.Long).Show();
        }
    }
}