using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDiaryPage : ContentPage
    {
        public AddDiaryPage(Route route, Diary diary = null)
        {
            InitializeComponent();
            if (diary == null)
            {
                this.Title = AppResources.AddDiaryPage_AddTitle;
            }
            else
            {
                this.Title = AppResources.AddDiaryPage_EditTitle;
            }
            this.BindingContext = new AddDiaryViewModel(route, diary);
            this.DatePicker.SetBinding(DatePicker.DateProperty, "Date");

            foreach (var style in (this.BindingContext as AddDiaryViewModel).Styles)
            {
                this.StylePicker.Items.Add(style);
            }
            this.StylePicker.SelectedIndex = (this.BindingContext as AddDiaryViewModel).SelectedStyleIndex;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            (this.BindingContext as AddDiaryViewModel).SaveCommand.Execute(null);
            await Navigation.PopAsync();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            this.NoteEditor.Focus();
        }
    }
}