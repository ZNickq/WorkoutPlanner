using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Diagnostics;

namespace WorkoutPlanner
{
    public partial class SettingsPage : PhoneApplicationPage
    {

        private static SettingsHandler sh;
        public static SettingsHandler GetSettingsHandler
        {
            get
            {
                if (sh == null)
                {
                    sh = new SettingsHandler();
                }
                return sh;
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pauseTextBox.Text = ""+GetSettingsHandler.pause_duration;
        }

        private void save_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            IsolatedStorageSettings iss = IsolatedStorageSettings.ApplicationSettings;
            GetSettingsHandler.pause_duration = Int32.Parse(pauseTextBox.Text);
            iss["pause_duration"] = GetSettingsHandler.pause_duration;
            iss.Save();
        }
    }
}