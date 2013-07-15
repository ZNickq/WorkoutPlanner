using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.ViewModels
{

    public class ExerciseType
    {
        private static List<string> _exerciseList;
        private static Dictionary<string, ExerciseType> _loaded;
        public static ExerciseType SQUAT = new ExerciseType("Squat", 1);
        public static ExerciseType LEG_PRESSING = new ExerciseType("Leg Pressing", 2);

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

        public int getDuration()
        {
            return _duration;
        }

        public string Title()
        {
            return _name;
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

        public ExerciseViewModel(ExerciseType type, int amount)
        {
            _type = type;
            _amount = amount;
            _duration = amount * _type.getDuration();
        }

        public int Amount
        {
            get
            {
                return _amount;
            }
        }

        public string Title
        {
            get
            {
                return _type.Title() + " - "+Amount+" times";
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
