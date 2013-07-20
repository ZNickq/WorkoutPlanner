using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner
{
    public class SettingsHandler
    {
        public int pause_duration;

        public SettingsHandler()
        {
            IsolatedStorageSettings iss = IsolatedStorageSettings.ApplicationSettings;
            if (iss.Count == 0)
            {
                iss["created"] = true;
                iss["pause-duration"] = 30;
                iss.Save();
            }
            pause_duration = (int)iss["pause-duration"];
        }
    }
}
