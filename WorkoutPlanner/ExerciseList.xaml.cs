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
    public partial class ExerciseList : PhoneApplicationPage
    {
        private Dictionary<string, ExerciseType> _got = new Dictionary<string, ExerciseType>();

        public ExerciseList()
        {
            InitializeComponent();

            List<string> aTypes = ExerciseType.ExerciseTypes;

            foreach (string type in aTypes)
            {
                PanoramaItem pi = new PanoramaItem();
                PanoramaUserControl puc = new PanoramaUserControl();

                List<string> ll = new List<string>();
                foreach (ExerciseType et in ExerciseType.getByType(type))
                {
                    ll.Add(et.Name);
                    _got.Add(et.Name, et);
                }
                puc.lSelector.ItemsSource = ll;
                pi.Header = type;
                pi.Content = puc;
                myP.Items.Add(pi);
            }

        }
    }
}