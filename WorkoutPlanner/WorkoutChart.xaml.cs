using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;

namespace WorkoutPlanner
{
    public partial class WorkoutChart : PhoneApplicationPage
    {
        List<WorkoutPerDay> lwpd = new List<WorkoutPerDay>();
        public WorkoutChart()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            WorkoutPerDay wpd = new WorkoutPerDay("25.7", 120);
            WorkoutPerDay wpd2 = new WorkoutPerDay("24.7", 160);
            lwpd.Add(wpd);
            lwpd.Add(wpd2);
            iBrowser.Navigate(new Uri("/chart-site.html", UriKind.Relative));
            iBrowser.ScriptNotify += iBrowser_ScriptNotify;
        }

        void iBrowser_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Debug.WriteLine("Was notified!");
        }

        private class WorkoutPerDay
        {
            private string _day;
            private int _duration;

            public WorkoutPerDay(string day, int duration)
            {
                _day = day;
                _duration = duration;
            }

            public string Day
            {
                get
                {
                    return _day;
                }
            }

            public int Duration
            {
                get
                {
                    return _duration;
                }
            }


        }
    }
}