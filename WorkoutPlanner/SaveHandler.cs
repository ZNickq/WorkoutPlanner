﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WorkoutPlanner.ViewModels;

namespace WorkoutPlanner
{
    class SaveHandler
    {
        public static async Task LoadUserImagesLocalDataAsync()
        {

            try

            {

                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<WorkoutViewModel>));

                var storage = IsolatedStorageFile.GetUserStoreForApplication();

                var fileStream = storage.OpenFile("save-data.bin", FileMode.OpenOrCreate, FileAccess.Read);

                List<WorkoutViewModel> toRet = (List<WorkoutViewModel>)deserializer.ReadObject(fileStream);
                fileStream.Close();

                if (toRet == null)
                {
                    return;
                }


                foreach (WorkoutViewModel wvm in toRet)
                {
                    App.ViewModel.Items.Add(wvm);
                }

            }

            catch(Exception ex)

            {
                string message = ex.Message;
            }

        }

  

        public static async System.Threading.Tasks.Task SaveUserImagesLocalDataAsync()

        {

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<WorkoutViewModel>));

            try

            {

                var allItems = App.ViewModel.Items;

  

                var storage = IsolatedStorageFile.GetUserStoreForApplication();
 

                var fileStream = storage.OpenFile("save-data.bin", FileMode.Create, FileAccess.ReadWrite);

  

                MemoryStream ms = new MemoryStream();

                ser.WriteObject(fileStream, allItems);

  

                fileStream.Close();

                ms.Close();

            }

            catch(Exception ex)

            {
                throw ex;
            }

        }


        public static async Task LoadWorkoutDataAsync()
        {
            try
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<WorkoutPlanner.WorkoutChart.WorkoutPerDay>));
                var storage = IsolatedStorageFile.GetUserStoreForApplication();
                var fileStream = storage.OpenFile("workout-data.bin", FileMode.OpenOrCreate, FileAccess.Read);
                List<WorkoutPlanner.WorkoutChart.WorkoutPerDay> toRet = (List<WorkoutPlanner.WorkoutChart.WorkoutPerDay>)deserializer.ReadObject(fileStream);
                fileStream.Close();
                if (toRet == null)
                {
                    return;
                }
                foreach (WorkoutPlanner.WorkoutChart.WorkoutPerDay wvm in toRet)
                {
                    WorkoutChart.addWorkoutPerDay(wvm, false);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public static async System.Threading.Tasks.Task SaveWorkoutDataAsync()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<WorkoutPlanner.WorkoutChart.WorkoutPerDay>));
            try
            {
                var allItems = WorkoutChart.lwpd;
                var storage = IsolatedStorageFile.GetUserStoreForApplication();
                var fileStream = storage.OpenFile("workout-data.bin", FileMode.Create, FileAccess.ReadWrite);
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(fileStream, allItems);
                fileStream.Close();
                ms.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


