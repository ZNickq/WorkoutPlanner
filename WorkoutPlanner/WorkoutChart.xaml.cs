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
using WorkoutPlanner.ViewModels;

namespace WorkoutPlanner
{
    public partial class WorkoutChart : PhoneApplicationPage
    {
        public static List<WorkoutPerDay> lwpd = new List<WorkoutPerDay>(); //TODO save

        public static void addWorkoutPerDay(WorkoutPerDay toAdd, Boolean save) {
            lwpd.Add(toAdd);
            if (save)
            {
                SaveHandler.SaveWorkoutDataAsync();
            }
        }


        public WorkoutChart()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            DateTime dt = DateTime.Today;

            string today = dt.Day + "/" + dt.Month;

            int todayDuration = 0;
            int allTimeRecord = 0;
            double average = 0;
            Dictionary<string, int> durationsPerDay = new Dictionary<string, int>();
            Dictionary<string, int> exerciseAparitions = new Dictionary<string, int>();
            string favExercise = "";
            int favExerciseCount = 0;
            foreach(WorkoutPerDay each in lwpd) {
                if(each.Day.Equals(today)) {
                    todayDuration+=each.Duration;
                }
                int wThisDay;
                if (durationsPerDay.ContainsKey(each.Day))
                {
                    int dd;
                    durationsPerDay.TryGetValue(each.Day, out dd);
                    dd += each.Duration;
                    durationsPerDay.Remove(each.Day);
                    durationsPerDay.Add(each.Day, dd);
                    wThisDay = dd;
                }
                else
                {
                    durationsPerDay.Add(each.Day, each.Duration);
                    wThisDay = each.Duration;
                }
                if (wThisDay > allTimeRecord)
                {
                    allTimeRecord = wThisDay;
                }
                foreach (string ss in each.Exes)
                {
                    int oneS;
                    if (!exerciseAparitions.TryGetValue(ss, out oneS))
                    {
                        oneS = 0;
                    }
                    else
                    {

                        exerciseAparitions.Remove(ss);
                    }
                    oneS++;
                    exerciseAparitions.Add(ss, oneS);
                    if (oneS > favExerciseCount)
                    {
                        favExerciseCount = oneS;
                        favExercise = ss;
                    }
                }
                average += each.Duration;
            }

            WToday.Text = "Worked Today: " + todayDuration;
            ATRecord.Text = "All-Time Record: " + allTimeRecord;
            AWDuration.Text = "Average Workout Duration: " + (average != 0 ? (average / lwpd.Count) : 0);
            FExercise.Text = "Favourite Exercise: "+favExercise;
            LExercises.Text = "Loaded Exercises: " + ExerciseType.ExerciseList.Count;
            NExercise.Text = "Newest Exercise: " + ExerciseType.lastExercise.Name;

        }

        public class WorkoutPerDay
        {
            private string _day;
            private HashSet<string> _exes = new HashSet<string>();
            private int _duration;

            public WorkoutPerDay(string day, int duration, HashSet<string> exes)
            {
                _day = day;
                _duration = duration;
                _exes = exes;
            }

            public WorkoutPerDay()
            {
            }

            public HashSet<string> Exes
            {
                get
                {
                    return _exes;
                }
                set
                {
                    _exes = value;
                }
            }

            public string Day
            {
                get
                {
                    return _day;
                }
                set
                {
                    _day = value;
                }
            }

            public int Duration
            {
                get
                {
                    return _duration;
                }
                set
                {
                    _duration = value;
                }
            }


        }

        private void on_clear(object sender, System.Windows.Input.GestureEventArgs e)
        {
            lwpd.Clear();
            SaveHandler.SaveWorkoutDataAsync();
            WToday.Text = "Worked Today: 0";
            ATRecord.Text = "All-Time Record: 0";
            AWDuration.Text = "Average Workout Duration: 0";
            FExercise.Text = "Favourite Exercise: ";
        }
    }
}