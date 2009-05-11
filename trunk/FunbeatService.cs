using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Janohl.ST2Funbeat.Funbeat;
using Janohl.ST2Funbeat;
using Janohl.ST2Funbeat.Settings;

namespace Janohl.Funbeat
{
    public class FunbeatService
    {

        private static FunbeatService instance;
        private static FunbeatService Instance
        {
            get
            {
                if (instance == null)
                    instance = new FunbeatService(Settings.Instance.User);
                return instance;
            }
        }

        private Login login;

        private FunbeatService(UserSettings user)
        {
            login = new Login();
            login.Password = user.Password;
            login.Username = user.Username;
            login.PasswordFormat = PasswordFormats.Clear;
        }

        public static int? SendTraining(DateTime startDate, TimeSpan duration, float? distance, string comment,
            int? hrAvg, int? hrMax, int? intensity, int kcal, string privateComment, int? repetitions, int? sets,
            int trainingType, TrackPoint[] trackPoints)
        {
            Training training = new Training();
            training.Comment = comment;
            training.Distance = distance;
            training.StartDateTime = startDate;
            training.Hours = duration.Hours;
            training.Minutes = duration.Minutes;
            training.Seconds = duration.Seconds;

            training.HRAvg = hrAvg;
            training.HRMax = hrMax;
            training.Intensity = intensity;
            training.KCal = kcal;
            training.PrivateComment = privateComment;
            training.Repetitions = repetitions;
            training.Sets = sets;
            training.TrainingTypeID = trainingType;
            training.TrackPoints = trackPoints;
            TrainingService client = new TrainingService();
            return client.AddTraining(Instance.login, training);
        }

        public static Dictionary<int, string> GetTrainingTypes()
        {
            TrainingService client = new TrainingService();
            TrainingType[] types = client.GetTrainingTypes();
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (TrainingType t in types)
                result.Add(t.ID, t.Name);

            return result;
        }

        public static List<FunbeatActivityType> FunbeatActivityTypes
        {
            get
            {
                List<FunbeatActivityType> fat = new List<FunbeatActivityType>();
                Dictionary<int, string> funbeatValues = FunbeatService.GetTrainingTypes();
                foreach (int i in funbeatValues.Keys)
                    fat.Add(new FunbeatActivityType(i, funbeatValues[i]));

                return fat;
            }
        }

    }
}
