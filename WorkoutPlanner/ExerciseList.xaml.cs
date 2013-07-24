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
    public partial class ExerciseList : PhoneApplicationPage
    {
        private Dictionary<string, ExerciseType> _got = new Dictionary<string, ExerciseType>();

        public ExerciseList()
        {
            InitializeComponent();

            HashSet<string> aTypes = ExerciseType.ExerciseTypes;

            foreach (string type in aTypes)
            {
                PanoramaItem pi = new PanoramaItem();
                PanoramaUserControl puc = new PanoramaUserControl();

                List<string> ll = new List<string>();
                List<ExerciseType> let = ExerciseType.getByType(type);
                foreach (ExerciseType et in let)
                {
                    ll.Add(et.Name);
                    _got.Add(et.Name, et);
                }
                LongListSelector LLS = puc.lSelector;
                LLS.SelectionChanged+= LLS_SelectionChanged;
                LLS.ItemsSource = ll;
                pi.Header = type;
                pi.Content = puc;
                myP.Items.Add(pi);
            }

        }

        private void LLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector LLS = sender as LongListSelector;
            // If selected item is null, do nothing
            if (LLS.SelectedItem == null)
                return;
            NavigationService.Navigate(new Uri("/ExerciseExplanation.xaml?exerciseToShow=" + LLS.SelectedItem.ToString(), UriKind.Relative));
            // Reset selected item to null
            LLS.SelectedItem = null;
    }
}
}