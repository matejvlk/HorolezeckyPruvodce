using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices;
using UHK.MatejVlk.HorolezeckyPruvodce.Tools;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels;
using UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.ItemViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
            this.BindingContext = new AccountViewModel();
        }

        protected override void OnAppearing()
        {
            CreateDiariesView();
        }

        private async void CreateDiariesView()
        {
            var vm = this.BindingContext as AccountViewModel;
            await vm.LoadData();
            if (vm.UserLoggedId)
            {
                this.DiariesStackLayout.Children.Clear();
                foreach (var item in vm.Diaries)
                {
                    this.DiariesStackLayout.Children.Add(this.CreateDiaryItem(item));
                }
            }
        }

        private StackLayout CreateDiaryItem(DiaryItemViewModel viewModel)
        {
            var diaryItemRootLayout = new StackLayout { Spacing = 0, Margin = 0, Orientation = StackOrientation.Vertical };
            diaryItemRootLayout.BindingContext = viewModel;

            var headerLayout = new StackLayout { Spacing = 0, Orientation = StackOrientation.Horizontal };

            var leftSideLayout = new StackLayout { Orientation = StackOrientation.Vertical, Margin = 10, HorizontalOptions = LayoutOptions.FillAndExpand };

            var lblDate = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = (double)App.Current.Resources["ListFontSize"],
                TextColor = (Color)App.Current.Resources["PrimaryDarkColor"]
            };
            lblDate.SetBinding(Label.TextProperty, "Date");
            leftSideLayout.Children.Add(lblDate);

            var lblSectorNames = new Label
            {
                FontSize = 15,
                TextColor = (Color)App.Current.Resources["PrimaryDarkColor"]
            };
            lblSectorNames.SetBinding(Label.TextProperty, "SectorNames");
            leftSideLayout.Children.Add(lblSectorNames);
            headerLayout.Children.Add(leftSideLayout);

            var lblNumberOfRoutes = new Label
            {
                FontSize = 15,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = (Color)App.Current.Resources["PrimaryDarkColor"]
            };
            lblNumberOfRoutes.SetBinding(Label.TextProperty, "NumberOfRoutes");
            headerLayout.Children.Add(lblNumberOfRoutes);

            var expandIcon = new Image
            {
                Margin = new Thickness(15, 10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                Aspect = Aspect.AspectFit,
                Source = "ic_expand_more_blue.png"
            };
            headerLayout.Children.Add(expandIcon);

            diaryItemRootLayout.Children.Add(headerLayout);

            var expandedLayout = new StackLayout { Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.FillAndExpand, Spacing = 0 };

            foreach (var diary in viewModel.ListOfSameDateDiaries)
            {
                var routeLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0,
                    Orientation = StackOrientation.Horizontal
                };

                var dotIcon = new Image
                {
                    Margin = new Thickness(15, 10, 10, 10),
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "ic_dot_blue.png"
                };
                routeLayout.Children.Add(dotIcon);

                var lblDiaryText = new Label
                {
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = (Color)App.Current.Resources["PrimaryDarkColor"],
                    Text = diary.DiaryText
                };
                var tgrRouteDetail = new TapGestureRecognizer();
                tgrRouteDetail.Tapped += async (sender, args) =>
                {
                    await Navigation.PushAsync(new RouteDetailPage(diary.Route));
                };
                lblDiaryText.GestureRecognizers.Add(tgrRouteDetail);
                routeLayout.Children.Add(lblDiaryText);

                var editIcon = new Image
                {
                    Margin = new Thickness(10),
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "ic_edit_blue.png"
                };
                var tgrEditDiary = new TapGestureRecognizer();
                tgrEditDiary.Tapped += async (sender, args) =>
                {
                    await Navigation.PushAsync(new AddDiaryPage(diary.Route, diary.Diary));
                };
                editIcon.GestureRecognizers.Add(tgrEditDiary);
                routeLayout.Children.Add(editIcon);

                var deleteIcon = new Image
                {
                    Margin = new Thickness(10),
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "ic_delete_red.png"
                };
                var tgrDeleteDiary = new TapGestureRecognizer();
                tgrDeleteDiary.Tapped += (sender, args) =>
                {
                    DependencyService.Get<IDialogs>().ShowDialog(AppResources.DiaryDeleteDialog_Title, AppResources.DiaryDeleteDialog_Message, AppResources.Ok, AppResources.Cancel, async (sender2, args2) =>
                    {//smazání cesty z deníčku
                        await App.Database.DeleteDiary(diary.Diary);
                        this.CreateDiariesView();
                    }, (sender3, args3) =>
                    {
                        //zrušení dialogu - nic neděláme
                    });
                };
                deleteIcon.GestureRecognizers.Add(tgrDeleteDiary);
                routeLayout.Children.Add(deleteIcon);

                expandedLayout.Children.Add(routeLayout);
            }

            diaryItemRootLayout.Children.Add(expandedLayout);
            expandedLayout.SetBinding(StackLayout.IsVisibleProperty, new Binding("Expanded"));

            var tgrExpand = new TapGestureRecognizer();
            tgrExpand.Tapped += async (s, e) =>
            {
                if (viewModel.Expanded)
                {
                    expandIcon.RotateTo(0);
                    await expandedLayout.FadeTo(0, 500);
                    viewModel.Expanded = false;
                }
                else
                {
                    viewModel.Expanded = true;
                    expandIcon.RotateTo(180);
                    expandedLayout.FadeTo(1, 500);
                }
                this.RootScrollView.ScrollToAsync(diaryItemRootLayout, ScrollToPosition.Start, true);
            };
            headerLayout.GestureRecognizers.Add(tgrExpand);

            var separator = new BoxView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = (double)App.Current.Resources["SeparatorHeight"],
                BackgroundColor = (Color)App.Current.Resources["PrimaryColor"]
            };
            diaryItemRootLayout.Children.Add(separator);

            return diaryItemRootLayout;
        }

        private async void ExpandIcon_Tapped(object sender, EventArgs e)
        {
            var vm = (this.BindingContext as AccountViewModel);
            if (vm.Expanded)
            {
                this.ExpandIcon.RotateTo(0);
                await this.DetailLayout.FadeTo(0, 500);
                vm.Expanded = false;
            }
            else
            {
                vm.Expanded = true;
                this.ExpandIcon.RotateTo(180);
                this.DetailLayout.FadeTo(1, 500);
            }
        }
    }
}
