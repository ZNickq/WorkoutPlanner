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

namespace WorkoutPlanner
{
    public partial class AddExercise : PhoneApplicationPage
    {
        public AddExercise()
        {
            InitializeComponent();

            MyListPicker.ItemsSource = ExerciseType.ExerciseList;
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
                }
            }
        }

        private bool isValidNumber(string number)
        {
            int l = number.Length;
           // System.Diagnostics.Debug.WriteLine("It has "+l);
            for (int i = 0; i < l; i++)
            {
                char e = number.ElementAt<char>(i);
                if (!(e >= '0' && e <= '9'))
                {
                    //System.Diagnostics.Debug.WriteLine(e+" is no number!");
                    return false;
                }
            }
            return true;
        }

        private void add_button(object sender, RoutedEventArgs e)
        {
            string rawNumber = txtPhoneNumber.Text;
            //System.Diagnostics.Debug.WriteLine("Testing " + rawNumber);
            if (!isValidNumber(rawNumber))
            {
                //System.Diagnostics.Debug.WriteLine("False, wtf");
                txtPhoneNumber.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 0, 0));
                return;
            }
            int nr = 0;
            try
            {
                nr = Convert.ToInt32(rawNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //Doesn't do shit but don't care
                return;
            }
            (DataContext as WorkoutViewModel).addExercise(new ExerciseViewModel(ExerciseType.FromName((string) MyListPicker.SelectedItem), nr));
            NavigationService.GoBack();
        }
    }
}