using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorkoutPlanner.ViewModels;
using System.Diagnostics;

namespace WorkoutPlanner
{
    public partial class ActiveWorkout : PhoneApplicationPage
    {
        private int ticks_running = 0;
        private int current_exercise_number = 0;
        private ExerciseViewModel current_exercise_instance;
        private ExerciseViewModel[] allModels;
        public ActiveWorkout()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dt = new System.Windows.Threading.DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            ticks_running++; //TODO must check that there is always an exercise and amount can't be 0!!
            if (ticks_running < 1)
            {
                Countdown.Text = "" + (-ticks_running);
                return;
            }
            if (ticks_running == 1) //First tick, set it all up
            {
                CurExercise.Text = "Exercise: " + current_exercise_instance.Type.Name;
            }
            Countdown.Text = ""+(current_exercise_instance.Duration - ticks_running + 1);
            int finished = (ticks_running == 1 ? ticks_running : ticks_running - 1) / (current_exercise_instance.Duration / current_exercise_instance.Amount);
            Remaining.Text = "Remaining: " + (current_exercise_instance.Amount - finished);
            if (current_exercise_instance.Duration == ticks_running - 1)
            {
                ticks_running = -SettingsPage.GetSettingsHandler.pause_duration; //TODO set the layout to take a break
                CurExercise.Text = "Take a break!";
                Countdown.Text = "" + SettingsPage.GetSettingsHandler.pause_duration;
                current_exercise_number++;
                //Debug.WriteLine("Current exercise " + current_exercise_number+" from "+allModels.Length);
                if (current_exercise_number == allModels.Length)
                {
                    CurExercise.Text = "Congratulations!";
                    Remaining.Text = "Workout finished.";
                    Countdown.Text = "";
                    (sender as System.Windows.Threading.DispatcherTimer).Stop();
                    DateTime now = DateTime.Now;

                    HashSet<string> exx = new HashSet<string>();
                    foreach (ExerciseViewModel evm in allModels)
                    {
                        exx.Add(evm.Type.Name);
                    }
                    WorkoutPlanner.WorkoutChart.WorkoutPerDay wpd = new WorkoutPlanner.WorkoutChart.WorkoutPerDay(now.Day+"/"+now.Month, (DataContext as WorkoutViewModel).DurationTime, exx);
                    WorkoutChart.addWorkoutPerDay(wpd, true);
                    return;
                }
                current_exercise_instance = allModels[current_exercise_number];
                //Debug.WriteLine("AAA "+current_exercise_instance.Type.Name+" for "+current_exercise_number);
                return;
            }

            
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("workout", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items[index];
                    current_exercise_instance = (DataContext as WorkoutViewModel).LoadedExercises.First();
                    allModels = (DataContext as WorkoutViewModel).LoadedExercises.ToArray();


                    CurExercise.Text = "Exercise: " + current_exercise_instance.Type.Name;
                    Countdown.Text = ""+current_exercise_instance.Duration;
                    Remaining.Text = "Remaining: "+current_exercise_instance.Amount;

                }
            }
        }
    }
}