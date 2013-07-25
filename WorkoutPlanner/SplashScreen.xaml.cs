using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WorkoutPlanner
{
    public partial class SplashScreen : PhoneApplicationPage
    {
        private static Boolean _dataLoaded;
        private static SplashScreen instance;
        public static Boolean DataLoaded
        {
            set
            {
                _dataLoaded = value;
                if (_dataLoaded == true)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        instance.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        instance.NavigationService.RemoveBackEntry();
                    });
                }
            }
            get
            {
                return _dataLoaded;
            }
        }


        public SplashScreen()
        {
            InitializeComponent();
            instance = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
    }
}