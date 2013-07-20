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
    public partial class AddWorkout : PhoneApplicationPage
    {
        public AddWorkout()
        {
            InitializeComponent();
        }

        private void on_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string tt = txtWorkoutName.Text;
            App.ViewModel.Items.Add(new ViewModels.WorkoutViewModel(tt));
            SaveHandler.SaveUserImagesLocalDataAsync();
            NavigationService.GoBack();
        }
    }
}