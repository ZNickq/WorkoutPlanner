﻿using System;
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

        public static void resetNewID(int newID) {
            new_ID = newID;
        }

        public WorkoutViewModel()
        {
            _id = new_ID;
            new_ID++;
        }

        public WorkoutViewModel(string workoutName)
        {
            _id = new_ID;
            new_ID++;

            WorkoutName = workoutName;
        }

        private int _id;

        /// <summary>
        /// Sample ViewModel property; this property is used to identify the object.
        /// </summary>
        /// <returns></returns>
        public int getID()
        {
            return _id;
        }

        public void setID(int newID)
        {
            _id = newID;
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

        public int DurationTime
        {
            get
            {
                return _durationTime;
            }
            set
            {
                _durationTime = value;
            }
        }
        
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
            set
            {
                _loadedExercises = value;
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
    }
}