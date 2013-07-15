using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WorkoutPlanner.Resources;

namespace WorkoutPlanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<WorkoutViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<WorkoutViewModel> Items { get; private set; }

        public string AddIconSource
        {
            get
            {
                bool dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
                if (dark)
                {
                    return "/Images/add-dark.png";
                }
                return "/Images/add-light.png";
            }
        }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            WorkoutViewModel toAdd = new WorkoutViewModel("Sample Routine");

            ExerciseViewModel evm = new ExerciseViewModel(ExerciseType.LEG_PRESSING, 5);

            toAdd.addExercise(evm);
            this.Items.Add(toAdd);
       
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}