using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WorkoutPlanner.ViewModels
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        private static int new_ID = 0;

        public WorkoutViewModel(string workoutName)
        {
            ID = new_ID;
            new_ID++;

            WorkoutName = workoutName;
        }

        private int _id;
        /// <summary>
        /// Sample ViewModel property; this property is used to identify the object.
        /// </summary>
        /// <returns></returns>
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _workoutName;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string WorkoutName
        {
            get
            {
                return _workoutName;
            }
            set
            {
                if (value != _workoutName)
                {
                    _workoutName = value;
                    NotifyPropertyChanged("WorkoutName");
                }
            }
        }

        private int _durationTime;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string DurationLine
        {
            get
            {
                return "Duration: "+_durationTime+" seconds";
            }
        }

        private List<ExerciseViewModel> _loadedExercises = new List<ExerciseViewModel>();

        public List<ExerciseViewModel> LoadedExercises
        {
            get
            {
                return _loadedExercises;
            }
        }

        public void addExercise(ExerciseViewModel ex)
        {
            _loadedExercises.Add(ex);
            _durationTime += ex.Duration;
            NotifyPropertyChanged("DurationLine");
            NotifyPropertyChanged("LoadedExercises");

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
    }
}