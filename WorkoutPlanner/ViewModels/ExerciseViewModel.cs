using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.ViewModels
{

    public class ExerciseType
    {


        public static ExerciseType lastExercise;

        public static async Task DownloadExerciseListAsync()
        {
            HttpClient client = new HttpClient();
            string url = "https://dl.dropboxusercontent.com/u/13921141/WorkoutPlanner/api.json";
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            
            JArray x = JArray.Parse(content);
            foreach (JObject jo in x)
            {
                string title = (string) jo.GetValue("title");
                int dur = (int) jo.GetValue("duration");
                string type = (string) jo.GetValue("type");
                string murl = (string) jo.GetValue("url");
                ExerciseType et = new ExerciseType(title, type, dur);
                lastExercise = et;
                et._url = murl;
            }

            //Now load data
            await SaveHandler.LoadUserImagesLocalDataAsync();

            SplashScreen.DataLoaded = true;

        }

        private static List<string> _exerciseList;
        private static Dictionary<string, ExerciseType> _loaded;
        private static HashSet<string> exTypes = new HashSet<string>();
        private static Dictionary<string, List<ExerciseType>> byType = new Dictionary<string, List<ExerciseType>>();

        private string _name;
        private string _type;
        private string _url;
        private int _duration;
        private ExerciseType(string name, string type, int duration) {
            _name = name;
            _type = type;
            _duration = duration;
            if (_exerciseList == null)
            {
                _exerciseList = new List<string>();
                _loaded = new Dictionary<string, ExerciseType>();
            }
            _exerciseList.Add(name);
            _loaded.Add(name, this);
            exTypes.Add(type);
            
            List<ExerciseType> ll;
            if (!byType.TryGetValue(type, out ll))
            {
                ll = new List<ExerciseType>();
                byType.Add(type, ll);
            }
            ll.Add(this);
        }

        public ExerciseType()
        {
        }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
    }

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public static List<ExerciseType> getByType(string type)
        {
            List<ExerciseType> toRet;
            byType.TryGetValue(type, out toRet);
            return toRet;
        }

        public static HashSet<string> ExerciseTypes
        {
            get
            {
                return exTypes;
            }
        }

        public static List<string> ExerciseList
        {
            get
            {
                return _exerciseList;
            }
        }

        internal static ExerciseType FromName(string p)
        {
            ExerciseType toRet;
            _loaded.TryGetValue(p, out toRet);
            return toRet;
        }
    };

    public class ExerciseViewModel
    {
        private ExerciseType _type;
        private int _duration;
        private int _amount;

        public ExerciseViewModel()
        {
        }

        public ExerciseViewModel(ExerciseType type, int amount)
        {
            _type = type;
            _amount = amount;
            _duration = amount * _type.Duration;
        }

        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        public string Title
        {
            get
            {
                return _type.Name + " - "+Amount+" times";
            }
        }

        public ExerciseType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
            }
        }

    }
}
