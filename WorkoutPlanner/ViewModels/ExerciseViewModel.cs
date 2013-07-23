using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.ViewModels
{

    public class ExerciseType
    {

        //https://dl.dropboxusercontent.com/u/13921141/WorkoutPlanner/api.json
        public static async Task DownloadExerciseListAsync()
        {
        }

        private static List<string> _exerciseList;
        private static Dictionary<string, ExerciseType> _loaded;
        //public static ExerciseType SQUAT = new ExerciseType("Squat", 1);
        //public static ExerciseType LEG_PRESSING = new ExerciseType("Leg Pressing", 2);
        //public static ExerciseType ARM_CIRCLES = new ExerciseType("Arm Circles", 1);

        private string _name;
        private int _duration;
        private ExerciseType(string name, int duration) {
            _name = name;
            _duration = duration;
            if (_exerciseList == null)
            {
                _exerciseList = new List<string>();
                _loaded = new Dictionary<string, ExerciseType>();
            }
            _exerciseList.Add(name);
            _loaded.Add(name, this);
        }

        public ExerciseType()
        {
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
